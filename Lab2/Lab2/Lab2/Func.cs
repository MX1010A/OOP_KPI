﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab2
{
    public class Func
    {
        public static void FileWrite(List<OfficeAppliances> officeAppliances, string path, FileMode fm)
        {
            var stream = File.Open(path, fm);
            var bf = new BinaryFormatter();
            foreach (var phoneCall in officeAppliances)
                bf.Serialize(stream, phoneCall); 
            stream.Close();
        }

        public static List<OfficeAppliances> FileRead(string path)
        {
            var officeAppliances = new List<OfficeAppliances>();
            var bf = new BinaryFormatter();
            var stream = File.Open(path, FileMode.Open);
            while (stream.Position < stream.Length)
                officeAppliances.Add((OfficeAppliances) bf.Deserialize(stream));
            stream.Close();
            return officeAppliances;
        }

        public static void ListOut(List<OfficeAppliances> list, string comment)
        {
            Console.WriteLine("\n" + comment);
            for (int i = 0; i < list.Count; i++) 
                Console.WriteLine(list[i].String());
            Console.WriteLine();
        }
        
        public static string Tab(int len) //форматування табуляції
        {
            string tab = "\t";
            for (int k = 0; k < (len >= 32 ? 0 : Math.Abs(len/10 - 3)); k++)
                tab += "\t";
            return tab;
        }

        public static List<OfficeAppliances> ListSort(string path1, string path2)
        {
            var list = FileRead(path1);
            var validList = new List<OfficeAppliances>();
            var expiredList = new List<OfficeAppliances>();
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].IsWarrantyExpired())
                    expiredList.Add(list[i]);
                else validList.Add(list[i]);
            }
            FileWrite(expiredList, path2, FileMode.Create);
            return validList;
        }
    }
}