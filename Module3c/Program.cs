using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Module3c
{
    //************************************************
    public class Program
    {  
        private static int iterationCounter=0;

        public static void Main()
        {
            string last, first;
            DateTime birthday;
            string department;
            string[] degreesOffered;
            string courseName;
            int courseID;
            int courseHours;

            getStudentInfo(out last, out first, out birthday);
            getTeacherInfo(out last, out first, out birthday);
            getProgramInfo(out department, out degreesOffered);
            getCourseInfo(out courseName, out courseID, out courseHours);

            genericResponse("Press any key to continue...");
        }

        private static void getStudentInfo(out string last, out string first, 
            out DateTime birthday)
        {
            last = genericResponse("Last Name");
            first = genericResponse("First Name");
            birthday = genericDate("Birthday");
            PrintStudentDetails(first, last, birthday);
        }

        static void PrintStudentDetails(string first, string last, DateTime birthday)
        {
            Console.WriteLine("");
            Console.WriteLine("{0} {1} was born on: {2}", first, last, birthday.ToString());
        }

        private static void getTeacherInfo(out string last, out string first, out DateTime birthday)
        {
            last = genericResponse("Last Name");
            first = genericResponse("First Name");
            birthday = genericDate("Birthday");
            PrintStudentDetails(first, last, birthday);
        }

        private static void getProgramInfo(out string department, 
            out string[] degreesOffered)
        {
            department = genericResponse("Department Name");
            string tmp = genericResponse("Degrees offered (Enter as comma separated list)");
            string pattern = ", ";
            Regex re = new Regex(pattern);
            degreesOffered = re.Split(tmp);

            PrintProgramDetails(department, degreesOffered);
        }

        static void PrintProgramDetails(string department, string[] degreesOffered)
        {
            Console.WriteLine("");
            Console.WriteLine("{0} offers the following Degree Programs", department);
            for (int i = 0; i < degreesOffered.Length; i++)
            {
                Console.WriteLine(degreesOffered[i]);
            }
        }

        private static void getCourseInfo(out string name, out int ID, out int hours)
        {
            name = genericResponse("Course Name");
            ID = int.Parse(genericResponse("Course ID number"));
            hours = int.Parse(genericResponse("Credit hours"));
            PrintCourseDetails(name, ID, hours);
        }

        private static void PrintCourseDetails(string name, int ID, int hours)
        {
            Console.WriteLine("");
            Console.WriteLine("{0} (ID: {1}) credit hours: {2}", name, ID.ToString(), hours.ToString());
        }

        private static string genericResponse(string Prompt)
        {
            Console.WriteLine(Prompt);
            return (Console.ReadLine());
        }

        private static DateTime genericDate(string Prompt)
        {
            iterationCounter++;
            string tmp = genericResponse(Prompt + " (Req'd format: m/d/yyyy)");
            Regex re = new Regex("/");

            string[] dateVals;
            int year, month, day;
            bool valid;

            try
            {
                // may fail if wrong delimiter, non-numeric, bad numeric values (e.g month 33)
                dateVals = re.Split(tmp);
                if ((dateVals.Length != 3) && (iterationCounter<3)) {
                    return (genericDate(Prompt));
                } else if (dateVals.Length != 3) {
                    throw new Exception("Improper date format provided");
                }

                try {
                    year = int.Parse(dateVals[2]);
                    month = int.Parse(dateVals[0]);
                    day = int.Parse(dateVals[1]);

                    valid = rangeCheck(year, DateTime.Now.Year-100, DateTime.Now.Year);
                    valid = valid && rangeCheck(month, 1, 12);
                    valid = valid && rangeCheck(day, 1, 31);

                    if (!valid) {
                        return (genericDate(Prompt));                        
                    }
                } catch {
                    if ((dateVals.Length != 3) && (iterationCounter<3)) {
                        return (genericDate(Prompt));
                    } else {
                        throw new Exception("Improper date provided");
                    }
                }
            }
            catch            
            {
                throw new Exception("F.U.B.A.R.");
            }
            iterationCounter = 0;

            return(new DateTime(year, month, day));
        }

        private static bool rangeCheck(int value, int min, int max)
        {
            return ((min <= value) && (value <= max));
        }
    }  
}