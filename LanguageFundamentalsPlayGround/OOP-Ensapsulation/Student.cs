namespace OOP_Ensapsulation
{
    public class Student
    {
        private string name,email,phone;
        private int age;
        public void SetName(string Name){
            if(string.IsNullOrEmpty(Name)){
                throw new Exception("Invalid Name");
            }
            name=Name;
        }
        public void SetEmail(string Email){
            if(!Email.Contains("@")){
                 throw new Exception("Invalid Email");
            }
            email=Email;
        }
        public void SetPhone(string Phone){
            if(Phone.Length>11 || Phone.Length<11){//09 256 275 319
            throw new Exception("Invalid Phone");
            }
            phone=Phone;
        }
        public void SetAge(int Age){
            if(Age<0 || Age>100){
                 throw new Exception("Invalid Age.");
            }
            age=Age;
        }
        public void ShowInfo(){
            Console.WriteLine("Student Information");
            Console.WriteLine("Name:"+name);
            Console.WriteLine("Email:"+email);
            Console.WriteLine("Phone:"+phone);
            Console.WriteLine("Age:"+age);
        }
    }
}