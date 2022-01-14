// See https://aka.ms/new-console-template for more information
Console.WriteLine("C# Async Basic");

await BasicAsync(); // Task 가 기다리길 기다리도록 await(없다면 Task가 끝나는 걸 기다리지 않음) await를 사용안하려면 메서드를 async를 넣지않고 만들자

await BasicExeptionAsync(); // 에러가 나는 곳을 확인하자

BasicRotateMatrices(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 });


/// <summary>
/// async void는 사용하지 말고 반환값이 없다면 Task를 사용하자
/// </summary>
async Task BasicAsync()
{
        int value = 0;

        Console.WriteLine("1초 휴식 시작");
        //비동기적으로 1초를 대기
        await Task.Delay(TimeSpan.FromSeconds(1));
        Console.WriteLine("1초 휴식 끝");

        value += 2;
        await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false); //UI가 존재하는 프로그램에서는 이렇게 사용하자

        Console.WriteLine(value.ToString());


}

/// <summary>
/// 오류처리가 필요한데 async는 예외를 Task에 배치한 후 await가 반환받는다. 
/// </summary>
async Task BasicExeptionAsync()
{
        // 예외는 바로 일어나지 않고 Task에서 발생한다.
        Task task = PossibleExeptionAsync();
        try
        {
                //여기에서 예외 발생
                await task;
        }
        catch (NotImplementedException ex)
        {
                Console.WriteLine(ex.ToString());
        }

}

async Task PossibleExeptionAsync() // 역시 async일때 await에 반환이 가능하고 아니면 첫 호출한 곳으로 바로 에러가 반환된다.
{
        await Task.Delay(TimeSpan.FromSeconds(1));
        throw new NotImplementedException();
}

void BasicRotateMatrices(IEnumerable<int> values)
{
        Parallel.ForEach(values, x => Console.Write(x)); // 병렬로 실행되며 2번째에는 실행할 것을 넣자
        Console.WriteLine();
        var result = values.AsParallel().Select(x => x); // linq로 선택된것을 병렬로 가져온다 순서가 중요하면 보통의 foreach를 사용하자
        foreach(var v in result)
                Console.Write(v.ToString());
        Console.WriteLine();

        try
        {
                Parallel.Invoke(() => { throw new Exception(); }, () => { throw new Exception(); });
        }
        catch (AggregateException ex)// Parallel.Invoke는 어떤 순서에서 에러가 났는지 확인하기 힘들지만 이 오류처리 코드를 통해 쉽게 만들 수 있다.
        { 
                ex.Handle(exception =>
                {
                        Console.WriteLine(exception);
                        return true;
                });
        }
}
