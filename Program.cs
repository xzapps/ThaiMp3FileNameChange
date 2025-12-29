using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ThaiMp3FileNameChange
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            var vowel = new Dictionary<string, string>
                {
                    { "เ-ียะ","เ-ียะ"} ,
                    { "เ-ีย","เ-ีย"} ,
                    { "เ-ือะ","เ-ือะ"} ,
                    { "เ-ือ","เ-ือ"} ,
                    { "-ัวะ","-ัวะ"} ,
                    { "-ัว","-ัว"}
                };

            var high = new Dictionary<string, string>
                    {
                    { "ง","ง"} ,
                    { "น","น"} ,
                    { "ม","ม"} ,
                    { "ย","ย"} ,
                    { "ร","ร"} ,
                    { "ล","ล"} ,
                    { "ว","ว"}
                };

            string path = @"C:\Users\Michael\Desktop\低辅音二和复合元音";
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            // 文件名的升序
            Array.Sort(files, new FileNameSort());

            int i = 0;
            foreach (KeyValuePair<string, string> item in high)
            {
                foreach (var item2 in vowel)
                {
                    string thaiName = item2.Value.Replace("-", item.Value)+".mp3";
                    string newPath = files[i].Directory.FullName + "\\" + thaiName;
                    Console.WriteLine(files[i].FullName);
                    Console.WriteLine(newPath);
                    File.Move(files[i].FullName, newPath);
                    i++;
                }
                Console.Write("\n");
            }        


            Console.WriteLine("成功");


            Console.ReadKey();
        }
    }

    public class FileNameSort : IComparer
    {
        //调用DLL
        [System.Runtime.InteropServices.DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string param1, string param2);


        //前后文件名进行比较。
        public int Compare(object name1, object name2)
        {
            if (null == name1 && null == name2)
            {
                return 0;
            }
            if (null == name1)
            {
                return -1;
            }
            if (null == name2)
            {
                return 1;
            }
            return StrCmpLogicalW(name1.ToString(), name2.ToString());
        }
    }
}
