using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;
using DuloGames.UI.Tweens;

public class CastBar : MonoBehaviour {
		public enum Transition {
			Instant,
			Fade
		}
		
		public UIProgressBar ProgressBarL, ProgressBarR, ChargeBarL, ChargeBarR;
		public Image ProgressFillL, ProgressFillR, ChargeFillL, ChargeFillR;
		public Image GlowImage, FrameImage;

		Spell CurrentSpell;

		[SerializeField] private Image m_IconImage;
		[SerializeField] private Text m_TitleLabel;

		[SerializeField] private Transition m_InTransition = Transition.Instant;
		[SerializeField] private float m_InTransitionDuration = 0.1f;

		[SerializeField] private float m_HideDelay = 0.3f;

		public CanvasGroup m_GlowFrame, m_CanvasGroup;
		
		// Tween controls
		[NonSerialized] private readonly TweenRunner<FloatTween> m_FloatTweenRunner;
		
		// Called by Unity prior to deserialization, should not be called by users
		protected CastBar() {
			if (this.m_FloatTweenRunner == null)
				this.m_FloatTweenRunner = new TweenRunner<FloatTween>();
			
			this.m_FloatTweenRunner.Init(this);
		}

		private void Update() {
			if (CurrentSpell == null) {
				Hide();
			}
		}

		public void Show() {
            // Bring to the front
            UIUtility.BringToFront(this.gameObject);

            // Do the transition
            if (this.m_InTransition == Transition.Instant) {
				// Set the canvas group alpha
				this.m_CanvasGroup.alpha = 1f;
			} else {
				// Start a tween
				this.StartAlphaTween(1f, this.m_InTransitionDuration, true);
			}
		}	
		
		public void Hide() {
			if (this.m_CanvasGroup == null) return;
		this.m_CanvasGroup.alpha -= 0.2f;
		}

		public void HideInstant() {
			if (this.m_CanvasGroup == null) return;
			this.m_CanvasGroup.alpha = 0f;
		}

		public void StartAlphaTween(float targetAlpha, float duration, bool ignoreTimeScale) {
			if (this.m_CanvasGroup == null) return;
			
			var floatTween = new FloatTween { duration = duration, startFloat = this.m_CanvasGroup.alpha, targetFloat = targetAlpha };
			floatTween.AddOnChangedCallback(SetCanvasAlpha);
			floatTween.ignoreTimeScale = ignoreTimeScale;
			this.m_FloatTweenRunner.StartTween(floatTween);
		}

		protected void SetCanvasAlpha(float alpha) {
			this.m_CanvasGroup.alpha = alpha;
		}
		
		public void SetChannelAmount(float amount) {
			if (amount >= 1f) {
				GlowIcon();
				currentWait = 0f;
			} else {
				FadeIcon();
				// If channel is decaying
				if (amount < ProgressBarL.fillAmount) {
					if (amount <= 0) {
						currentWait = 0f;
						Hide();
					} else {
						HideWait();
					}
				} else {
					currentWait = 0f;
				}
			}

			this.ProgressBarL.fillAmount = amount;
			this.ProgressBarR.fillAmount = amount;
		}


		bool Flickering = false;

		public void SetChargeAmount(float amount) {
			this.ChargeBarL.fillAmount = amount;
			this.ChargeBarR.fillAmount = amount;

			if (amount >= 1f) {
				if (CurrentSpell.Info.HoldTo != 0) {
					Flickering = true;
					FlickerIcon();
				}
			} else {
				Flickering = false;
			}
		}
		
		public void SetSpell(Spell spell) {	
			CurrentSpell = spell;

			if (spell == null) return;
			UISpellInfo info = spell.Info;

			// Text
			this.m_TitleLabel.text = info.Name;

			// Colors
			ProgressFillL.color = info.ChannelColor;
			ProgressFillR.color = info.ChannelColor;

			GlowImage.color = info.ChannelColor;
			FrameImage.color = info.ChannelColor;

			ChargeFillL.color = info.ChargeColor;
			ChargeFillR.color = info.ChargeColor;

			// Icon
			this.m_IconImage.sprite = info.Icon;
		}

		public void ClearSpell() {
			CurrentSpell = null;
		}

		public void GlowIcon() {
			if (Flickering) return;;

			if (m_GlowFrame.alpha < 1) {
				m_GlowFrame.alpha += 0.1f;
			}
		}

		public void FadeIcon() {
			if (m_GlowFrame.alpha > 0) {
				m_GlowFrame.alpha -= 0.1f;
			}
		}

		float minGlow = 0.3f;
		bool Glowing = true;

		public void FlickerIcon() {
			var glowSpeed = 0.5f / (CurrentSpell.Info.HoldTo - CurrentSpell.holdCounter);
			if (Glowing) {
				if (m_GlowFrame.alpha > minGlow) {
					m_GlowFrame.alpha -= glowSpeed;
				} else {
					m_GlowFrame.alpha = minGlow;
					Glowing = false;
				}
			} else {
				if (m_GlowFrame.alpha < 1f) {
					m_GlowFrame.alpha += glowSpeed;
				} else {
					m_GlowFrame.alpha = 1f;
					Glowing = true;
				}
			}
		}

		float currentWait = 0f;
		void HideWait() {
			if (currentWait >= this.m_HideDelay) {
				Hide();
			} else {
				currentWait += 0.05f;
			}

		}
}
