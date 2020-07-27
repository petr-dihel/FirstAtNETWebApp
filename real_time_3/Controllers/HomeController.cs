using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using real_time_3.Models;

namespace real_time_3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Form form)
        {
            if (ModelState.IsValid)
            {
                string path = Path.GetFullPath("test.txt");
                string line = form.Name + ";" + form.Surname + ";" + form.Email + ";" + form.Text + "|";
                Console.WriteLine("debug1: " + path);
                System.IO.File.AppendAllText(path, line);
                ViewBag.message = "Odesláno";
            }
            else {
                ViewBag.message = "Prosím opravte údaje ";
            }
            return View();
        }

        public IActionResult ShowList()
        {
            string path = Path.GetFullPath("test.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            List<Form> showList = new List<Form>();          
            foreach (string line in lines)
            {
                foreach (string row in line.Split("|"))
                {
                    if (row != "")
                    {
                        string[] values = row.Split(";");
                        Form form = new Form(); form.Name = values[0];
                        form.Surname = values[1];
                        form.Email = values[2];
                        form.Text = values[3];
                        showList.Add(form);
                    }
                   
                }
                
            }
            ViewBag.list = showList;
            return View();
        }

        public IActionResult Delete(string email)
        {
            string path = Path.GetFullPath("test.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            List<string> newLines = new List<string>();
            foreach (string line in lines)
            {
                foreach (string row in line.Split("|"))
                {
                    if (row != "")
                    {
                        string[] values = row.Split(";");
                        if (values[2] != email)
                        {
                            newLines.Add(row);
                        }
                    }
                  
                }
            }

            System.IO.File.WriteAllLines(path, newLines);
            ViewBag.message = "Úspěšně smazáno";
            RedirectToAction("ShowList");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
