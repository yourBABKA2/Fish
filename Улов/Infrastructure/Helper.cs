using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Улов.Entities;

namespace Улов.Infrastructure
{
    public class Helper
    {
        public static Context DbContext;

        public static void ImportImage()
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory + "\\import";
            foreach (var file in Directory.GetFiles($"{exePath}"))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                foreach (var item in DbContext.Catalog.ToList())
                {
                    if(item.Article == fileName)
                        item.Photo = File.ReadAllBytes(file);
                }
            }
            SaveDb();
        }

        private static void SaveDb()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
    }
}
