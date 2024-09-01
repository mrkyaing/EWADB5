public class Person{
    public void SayHello(string name){
        if(string.IsNullOrEmpty(name)){
            throw new Exception("Invalid Name");
        }else{
            Console.WriteLine("Hello,"+name+" Nice to see you");
        }
    }
}