// See https://aka.ms/new-console-template for more information
Console.WriteLine("C# Async Basic");

await BasicAsync(); // Task 가 기다리길 기다리도록 await(없다면 Task가 끝나는 걸 기다리지 않음) await를 사용안하려면 메서드를 async를 넣지않고 만들자

await BasicExeptionAsync();



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

async Task PossibleExeptionAsync() // 역시 async일때 await에 반환이 가능하고 아니면 첫 호출한 곳으로 바로 에로가 반환된다.
{
        await Task.Delay(TimeSpan.FromSeconds(1));
        throw new NotImplementedException();
}