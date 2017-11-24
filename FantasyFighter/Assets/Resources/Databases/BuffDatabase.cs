using UnityEngine;

namespace DuloGames.UI
{
	public class BuffDatabase : ScriptableObject {

        #region singleton
        private static BuffDatabase m_Instance;
        public static BuffDatabase Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = Resources.Load("Databases/BuffDatabase") as BuffDatabase;

                return m_Instance;
            }
        }
        #endregion

        public Buff[] items;
		
		/// <summary>
		/// Get the specified ItemInfo by index.
		/// </summary>
		/// <param name="index">Index.</param>
		public Buff Get(int index)
		{
			return (this.items[index]);
		}
		
		/// <summary>
		/// Gets the specified ItemInfo by ID.
		/// </summary>
		/// <returns>The ItemInfo or NULL if not found.</returns>
		/// <param name="ID">The item ID.</param>
		public Buff GetByID(int ID)
		{
			for (int i = 0; i < this.items.Length; i++)
			{
				// if (this.items[i].ID == ID)
				// 	return this.items[i];
			}
			
			return null;
		}
	}
}
