using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien.BLL
{
    public class BLL_ID
    {
        private static BLL_ID _Instance;
        private BLL_ID()
        {
        }
        public static BLL_ID Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_ID();
                }
                return _Instance;
            }
            private set { }
        }
        public int kiemTraViTriCoTheThem(List<string> list_id)
        {
            List<int > list = new List<int>();
            for (int i = 0; i < list_id.Count; i++)
               list.Add(Int32.Parse(list_id[i]));
            list.Sort();
            for(int i = 0; i < list.Count; i++)
            {
                if (i+1!= list[i])
                {
                    return i + 1;
                } 
            }
            return list_id.Count + 1;
        }
        public string convertInt2Ma(int id)
        {
            string ma = id.ToString();
            for (int i = 0; i < 3 - ma.Length + 1; i++)
            {
                ma = "0" + ma;
            }
            return ma;
        }
    }
}
