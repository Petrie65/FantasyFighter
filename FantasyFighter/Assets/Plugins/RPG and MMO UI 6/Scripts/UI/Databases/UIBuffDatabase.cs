using UnityEngine;


namespace DuloGames.UI
{
	public class UIBuffDatabase : ScriptableObject {

        #region singleton
        private static UIBuffDatabase m_Instance;
        public static UIBuffDatabase Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = Resources.Load("Databases/BuffDatabase") as UIBuffDatabase;

                return m_Instance;
            }
        }
        #endregion

        public UIBuffInfo[] buffs;
		
		/// <summary>
		/// Get the specified ItemInfo by index.
		/// </summary>
		/// <param name="index">Index.</param>
		public UIBuffInfo Get(int index)
		{
			return (this.buffs[index]);
		}
		
		/// <summary>
		/// Gets the specified ItemInfo by ID.
		/// </summary>
		/// <returns>The ItemInfo or NULL if not found.</returns>
		/// <param name="ID">The item ID.</param>
		public UIBuffInfo GetByID(int ID)
		{
			for (int i = 0; i < this.buffs.Length; i++)
			{
				if (this.buffs[i].ID == ID)
					return this.buffs[i];
			}
			
			return null;
		}

		/// <summary>
		/// Gets the specified SpellInfo by Name.
		/// </summary>
		/// <returns>The SpellInfo or NULL if not found.</returns>
		/// <param name="name">The spell name.</param>
		public UIBuffInfo GetByName(string name)
		{
			for (int i = 0; i < this.buffs.Length; i++)
			{
				if (this.buffs[i].Name == name)
					return this.buffs[i];
			}
	
			return null;
		}
	}
}
