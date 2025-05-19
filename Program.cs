using System.Diagnostics;

string[] ceps = new string[10];
ceps[0] = "07155081";
ceps[1] = "15800100";
ceps[2] = "38407369";
ceps[3] = "77445100";
ceps[4] = "78015818";
ceps[5] = "07155081";
ceps[6] = "15800100";
ceps[7] = "38407369";
ceps[8] = "77445100";
ceps[9] = "78015818";

var option = 4;

//~4000 ms
if (option == 1)
{
    var stopWatch = new Stopwatch();

    stopWatch.Start();
    var cepList = new List<CepModel>();

    foreach (var cep in ceps)
    {
        cepList.Add(await new ViaCepService().GetCepAsync(cep));
    }

    stopWatch.Stop();

    Console.WriteLine($"Elapsed Time {stopWatch.ElapsedMilliseconds} ms");

    cepList.ToList().ForEach(cep => Console.WriteLine(cep));
    Console.ReadKey();

}

//~1346 ms
else if (option == 2)
{
    var parallelOptions = new ParallelOptions();
    parallelOptions.MaxDegreeOfParallelism = 8;

    var stopWatch = new Stopwatch();

    stopWatch.Start();
    var cepList = new List<CepModel>();

    Parallel.ForEach(ceps, parallelOptions, cep =>
    {
        cepList.Add(new ViaCepService().GetCep(cep));
    });

    stopWatch.Stop();

    Console.WriteLine($"Elapsed Time {stopWatch.ElapsedMilliseconds} ms");

    cepList.ToList().ForEach(cep => Console.WriteLine(cep));
    Console.ReadKey();
}

//~805 ms
else if (option == 3)
{
    //The thing that slows down the process is thread handling (when using a lot of data).
    //Executing 1000 requests at the same time will try to create or utilize 1000 threads and managing them is a cost
    var stopWatch = new Stopwatch();

    stopWatch.Start();

    var cepTaskList = ceps.Select(cep => new ViaCepService().GetCepAsync(cep));

    var cepList = await Task.WhenAll(cepTaskList);

    stopWatch.Stop();

    Console.WriteLine($"Elapsed Time {stopWatch.ElapsedMilliseconds} ms");

    cepList.ToList().ForEach(cep => Console.WriteLine(cep));
    Console.ReadKey();
}

//~805 ms
else if (option == 4)
{
    //This is the slightly better result because framework needs to handle fewer threads at the same time and therefore it is more effective. 
    //This also is a better solution when working with a lot of data. If working with little data solution 3 should be better

    //A better sollution would be change the GetCepAsync to accept multiples ids at the same time. By using this, the calls to the api
    //would be reduced so the time would be.
    var stopWatch = new Stopwatch();

    stopWatch.Start();

    var cepList = new List<CepModel>();

    var batchSize = 100;
    int numberOfBatches = (int)Math.Ceiling((double)ceps.Count() / batchSize);

    for (int i = 0; i < numberOfBatches; i++)
    {
        var currentIds = ceps.Skip(i * batchSize).Take(batchSize);
        var cepTaskList = currentIds.Select(id => (new ViaCepService().GetCepAsync(id)));

        cepList.AddRange(await Task.WhenAll(cepTaskList));
    }

    stopWatch.Stop();

    Console.WriteLine($"Elapsed Time {stopWatch.ElapsedMilliseconds} ms");

    cepList.ToList().ForEach(cep => Console.WriteLine(cep));
    Console.ReadKey();
}
