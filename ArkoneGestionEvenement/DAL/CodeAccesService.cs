﻿using ArkoneGestionEvenement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkoneGestionEvenement.DAL
{
    public static class CodeAccesService
    {
        public static CodesAcce AddCodeAcces(CodesAcce code)
        {
            using(ArkoneGestionContext db = new ArkoneGestionContext())
            {
                db.CodesAcces.Add(code);
                db.SaveChanges();
                return code;
            }
        }
        
        public static string GenerateUniqueCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            string code;

            do
            {
                code = new string(Enumerable.Repeat(chars, 6)
                                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (CodeExists(code));

            return code;
        }

        private static bool CodeExists(string code)
        {
            using (ArkoneGestionContext db = new ArkoneGestionContext())
            {
                return db.CodesAcces.Any(e => e.Code == code);
            }
                
        }
    }
}
