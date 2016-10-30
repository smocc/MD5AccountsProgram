using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.Net;
using System.Text.RegularExpressions;

namespace md5Generator
{
    class Program
    {
        static bool log = false;
        static int lineFound;
        static string cPassHash;
        static string usrAddr= "C:\\Users\\atlan\\Documents\\accs\\usrs.txt";
        static string lstAddr = "C:\\Users\\atlan\\Documents\\accs\\lst.txt";
        static string usrsFile = System.IO.File.ReadAllText("C:\\Users\\atlan\\Documents\\accs\\usrs.txt");
        static string lstFile = System.IO.File.ReadAllText("C:\\Users\\atlan\\Documents\\accs\\lst.txt");
        static string nWrd;
        static string uname;
        static string pword;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.WriteLine("1. Create account.");
                Console.WriteLine("2. Login to a current account.");

                Console.WriteLine();
                Console.Write(" > ");
                string inp = Console.ReadLine();
                if (inp == "1")
                {
                    createAcc("");
                }
                else if (inp == "2")
                {
                    login("");
                }
            }
        }
        static string md5Gen(string text)
        {
            MD5 hash = new MD5CryptoServiceProvider();
            hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = hash.Hash;
            StringBuilder strBldr = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBldr.Append(result[i].ToString("x2"));
            }
            return strBldr.ToString();
        }
        static void login(string args)
        {
            Console.Write("Username: ");
            string uname = Console.ReadLine();
            Console.Write("Password: ");
            string pword = Console.ReadLine();
            while (true)
            {
                if (usrsFile.Contains(uname))
                {
                    if (lstFile.Contains(pword))
                    {
                        cPassHash = md5Gen(pword);
                        checkHash(cPassHash);
                        break;
                    } else
                    {
                        Console.WriteLine("Error...");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Error, incorrect username or password.");
                    Console.Write("Username: ");
                    uname = Console.ReadLine();
                    Console.Write("Password: ");
                    pword = Console.ReadLine();
                }
            }
        }
        static void createAcc(string args)
        {
            Console.Write("Username: ");
            string unameReg = Console.ReadLine();
            Console.Write("Password: ");
            string pwordReg = Console.ReadLine();
            Console.Write("Re-Enter Password: ");
            string pwordRegCheck = Console.ReadLine();
            while (true)
            {
                if (pwordReg == pwordRegCheck)
                {
                    break;
                } else
                {
                    Console.WriteLine();
                    Console.WriteLine("Incorrect, please enter passwords that are the same.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.Write("Password: ");
                    pwordReg = Console.ReadLine();
                    Console.Write("Re-Enter Password: ");
                    pwordRegCheck = Console.ReadLine();
                }
            }
            while (true) {
                if (System.IO.File.ReadAllText("C:\\Users\\atlan\\Documents\\accs\\usrs.txt").Contains(unameReg))
                {
                    Console.Write("That username is taken, please choose another username: ");
                    unameReg = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            string pwordHash = md5Gen(pwordReg);
            string newWrd = "\n" + pwordHash + " " + pwordReg;
            string newAcc = "\n" + pwordHash + ":" + unameReg;
            System.IO.File.AppendAllText("C:\\Users\\atlan\\Documents\\accs\\usrs.txt", newAcc + Environment.NewLine);
            if (System.IO.File.ReadAllText("C:\\Users\\atlan\\Documents\\accs\\lst.txt").Contains(newWrd))
            {
            }
            else
            {
                System.IO.File.AppendAllText("C:\\Users\\atlan\\Documents\\accs\\lst.txt", newWrd + Environment.NewLine);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Account successfully created.");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
            Console.Clear();
        }
        static void checkHash(string args)
        {
            int counter;
            if (lstFile.Contains(cPassHash))
            {
                Console.WriteLine("Found hash, attempting to decrypt.");
                Thread.Sleep(100);
                Console.WriteLine("Decrypting hash " + cPassHash + "...");
                Thread.Sleep(100);
                if (usrsFile.Contains(cPassHash))
                {
                    Console.WriteLine("Logged in as user " + uname + ".");
                    log = true;
                }
            }
        }
    }
}