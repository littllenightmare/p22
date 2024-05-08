using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p22.database
{
    public partial class Клиенты
    {
        public string PhotoFull
        {
            get
            {
                if (this.Фото == null)
                {
                    return null;
                }
                else
                {
                    string namePhoto = Directory.GetCurrentDirectory() + "\\image\\" + Фото;
                    return namePhoto;
                }
            }
        }
    }
}
