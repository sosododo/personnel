using personnel.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace personnel.ModelView
{

    class Emp_Maxsalary
    {
      


    public float getmaxsal(string cate)
        {
            float m;
            switch (cate)
            {
                                                 
                case "الثانية/مخبريين":
                  

                case "الثانية/اداريين":
                    m = 70780;
                    break;

                case "الثالثة":
                    m = 67480;
                    break;

                case "الرابعة":
                    m = 67480;
                    break;

                case "الخامسة":
                    m = 62530;
                    break;

                default:
                    m = 80240;
                    break;

            }

            return m;

       

    }
        public float getbigsal(string cate)
        {
            float b;
            switch (cate)
            {
                case "الأولى":
                    b = 53250;
                    break;
                case "الثانية/مخبريين":
                    b = 51550;
                    break;

                case "الثانية/اداريين":
                    b = 50940;
                    break;

                case "الثالثة":
                    b = 50430;
                    break;

                case "الرابعة":
                    b = 48015;
                    break;

                case "الخامسة":
                    b = 47675;
                    break;
                case "فنية":
                case "1/4":
                case "2/4":
                case "3/4":
                    b = 55225;
                    break;

                default:
                    b = 57495;
                    break;

            }

            return b;



        }

    }
}
