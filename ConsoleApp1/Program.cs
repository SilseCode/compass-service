using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using Npoi.Mapper;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.XSSF.UserModel;

namespace CompassSite.Database.Comparers
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public Category Category { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

   
    class Program
    {
        static void Main(string[] args)
        {
            List<int> ints = new List<int>();
            for (int i = 0; i < 25; i++)
            {
                ints.Add(i);
            }

            int page = 3;
            int PageSize = 10;
            foreach (var item in ints.Skip((page - 1) * PageSize).Take(PageSize).ToList())
            {
                Console.Write(item + " ");
            }
        }
    }
}