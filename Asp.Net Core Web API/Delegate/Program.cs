// See https://aka.ms/new-console-template for more information




public delegate void SampleDelegate(int x, int y);

public interface Isample
{
    void SampleAdd(int x, int y);
}

public class Sample : Isample
{

    public static void Main()
    {
       
    }

    public void Execute()
    {
        SampleDelegate sampleDelegate = SampleAdd;
        ExecuteDelegate(sampleDelegate, 1, 2);
    }
    public void SampleAdd(int x, int y)
    {
        Console.WriteLine("Sum = ", x + y) ;
    }

    public void ExecuteDelegate(SampleDelegate sampleDelegae, int x, int y)
    {
        sampleDelegae(x, y);
    }

    public void ExecuteDelegate1(int x, int y)
    {
        SampleDelegate sampleDelegate = SampleAdd;
        sampleDelegate.Invoke(x, y);
    }

}


