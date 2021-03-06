using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
    public interface IStudent
    {
        string StudentType { get; set; }
        string Name { get; set; } 
        int Age { get; set; } 
        bool IsInState { get; set; }
        bool IsGraded { get; set; } 
        int StudentIDNumber { get; set; }

        void SetStudentInfo();
    }

    public enum StudentTypes
    {
        Graduate,
        Undergraduate,
        NonDegreeSeeking
    }


    public static class StudentFactory
    {
        public static IStudent CreateStudent(StudentTypes studentType)
        {
            if(studentType == StudentTypes.Graduate) return new Graduate();
            else if(studentType == StudentTypes.Undergraduate) return new Undergraduate();
            else if(studentType == StudentTypes.NonDegreeSeeking) return new NonDegreeSeeking();
            else return null;
        }
    }

    public abstract class Student:IStudent
    {

        public string StudentType { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsInState { get; set; }
        public bool IsGraded { get; set; }
        public int StudentIDNumber { get; set; }

        public abstract void SetStudentInfo();

        //Need to add error handling if e.g. user enters a letter instead of numbers for age:
        public virtual void RequestStudentInfo() {
            Console.WriteLine("Please enter student information in order to create a new student.  All fields are required.");
            Console.WriteLine("\tName: ");
            this.Name = Console.ReadLine();
            Console.WriteLine("\tAge: ");
            this.Age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\tDoes student qualify for in-state tuition?  [Enter 1 for yes or 0 for no.]");
            char tmp = Console.ReadKey().KeyChar;
            if(tmp == '1') this.IsInState = true;
            else if(tmp == '0') this.IsInState = false;
            else {
                Console.WriteLine("Invalid input.  Student marked as out-of-state for now.");
                this.IsInState = false;
                Console.ReadKey(true);
            }         
        }

        //Need to add error handling in case the generated id is already in the student list.  Just generate a new one.
        public int GenerateNewID()
        {
            Random r = new Random();
            int id = r.Next(1, 100000);
            return id;
        }
    }

    

    public class Graduate : Student
    {
        public Graduate()
        {
            SetStudentInfo();
        }
        public override void SetStudentInfo()
        {
            IsGraded = true;
            this.StudentType = "Graduate";
            this.RequestStudentInfo();
            this.StudentIDNumber = GenerateNewID();
        }
    }

    public class Undergraduate: Student
    {
        public Undergraduate()
        {
            SetStudentInfo();
        }
        
        public override void SetStudentInfo()
        {
            this.IsGraded = true;
            this.StudentType = "Undergraduate";
            RequestStudentInfo();
            this.StudentIDNumber = GenerateNewID();
        }
    }

    public class NonDegreeSeeking : Student
    {
        public NonDegreeSeeking()
        {
            SetStudentInfo();
        }
        public override void SetStudentInfo()
        {
            this.IsGraded = false;
            this.StudentType = "Non-degree-seeking";
            RequestStudentInfo();
            this.StudentIDNumber = GenerateNewID();
        }
    }

}
