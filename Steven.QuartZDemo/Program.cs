using Steven.QuartZDemo;



try
{
    //Basic QuratZ
    //new BasicQuartZ().Show();

    //QuartZ 的 AOP
    //new QuartZAop().AopSHow();

    //QuartZ 的 DB
    new DbStoreQuartZ().Show();


    Console.ReadLine();
    Console.WriteLine("finish");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
     throw;
}