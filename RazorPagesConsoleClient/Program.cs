using Newtonsoft.Json;
using RazorPagesLibrary.DTO;
using RazorPagesLibrary.Model;
using System.Net.Http.Json;

const string BASE_URL = @"https://localhost:7003/";

var http = new HttpClient();
http.BaseAddress = new Uri(BASE_URL);

bool running = true;
while(running)
{
    WriteCommands();

    var key = Console.ReadKey();
    Console.WriteLine();

    await HandleKey(key);
}


void WriteCommands()
{
    Console.WriteLine("List of commands:");
    Console.WriteLine("p\t Get all Products");
    Console.WriteLine("c\t Get all Companies");
    Console.WriteLine("u\t Get all Users");
    Console.WriteLine("s\t Create new Sale");
    Console.WriteLine("e\t Update Sale");
    Console.WriteLine("q\t Quit");
}

async Task HandleKey(ConsoleKeyInfo keyInfo)
{
    Console.WriteLine();
    Console.WriteLine();
    switch (keyInfo.KeyChar)
    {
        case 'p': await WriteAllProducts(); break;
        case 'c': await WriteAllCompanies(); break;
        case 'u': await WriteAllUsers(); break;
        case 's': await CreateNewSale(); break;
        case 'e': await UpdateSale(); break;
        case 'q': running = false; break;
        default: Console.WriteLine("Bad command"); break;
    }
    Console.WriteLine();
    Console.WriteLine();
}

void DrawLine(int len) { Console.WriteLine(new string('-', len)); };

async Task WriteAllProducts()
{
    await Console.Out.WriteLineAsync("PRODUCTS");
    try
    {
        var res = await http.GetAsync("/api/water");
        var waters = await res.Content.ReadFromJsonAsync(typeof(IList<GetWaterResponse>)) as IList<GetWaterResponse>;
        foreach (var w in waters!)
        {
            DrawLine(120);
            Console.WriteLine($"{w.Id},\t {w.Name},\t {w.Type},\t {w.Manufacturer},\t {w.Mineralization},\t pH={w.pH}, Stock: {w.Stock}");
        }
        DrawLine(120);
    }
    catch (Exception ex)
    {
        await Console.Out.WriteLineAsync("Error");
        await Console.Out.WriteLineAsync(ex.Message);
    }
}

async Task WriteAllCompanies()
{
    await Console.Out.WriteLineAsync("COMPANIES");
    try
    {
        var res = await http.GetAsync("/api/company");
        var companies = await res.Content.ReadFromJsonAsync(typeof(IList<Company>)) as IList<Company>;
        foreach (var c in companies!)
        {
            DrawLine(120);
            Console.WriteLine($"{c.Id},\t {c.Name},\t {c.Email},\t {c.PhoneNumber}");
        }
        DrawLine(120);
    }
    catch (Exception ex)
    {
        await Console.Out.WriteLineAsync("Error");
        await Console.Out.WriteLineAsync(ex.Message);
    }
}

async Task WriteAllUsers()
{
    await Console.Out.WriteLineAsync("USERS");
    try
    {
        var res = await http.GetAsync("/api/user");
        var users = await res.Content.ReadFromJsonAsync(typeof(IList<string>)) as IList<string>;
        DrawLine(50);
        foreach (var u in users!)
        {
            Console.WriteLine(u);
        }
        DrawLine(50);
    }
    catch (Exception ex)
    {
        await Console.Out.WriteLineAsync("Error");
        await Console.Out.WriteLineAsync(ex.Message);
    }
}

async Task CreateNewSale()
{
    try
    {
        await Console.Out.WriteLineAsync("NEW SALE");
        await Console.Out.WriteAsync("Enter client name: ");
        var name = await Console.In.ReadLineAsync();

        var req = new CreateSaleRequest();
        req.ClientName = name!;
        req.SaleUnits = new List<SaleUnitDTO>();

        await Console.Out.WriteLineAsync("\n\nAdd products:");
        await Console.Out.WriteLineAsync("q\t Stop adding");
        bool addingUnits = true;
        while(addingUnits)
        {
            await Console.Out.WriteAsync("Product ID: ");
            var idStr = await Console.In.ReadLineAsync();
            if (!int.TryParse(idStr!, out int id)) break;

            await Console.Out.WriteAsync("Amount: ");
            var countStr = await Console.In.ReadLineAsync();
            if (!int.TryParse(countStr!, out int count)) break;

            req.SaleUnits.Add(new SaleUnitDTO()
            {
                WaterId = id,
                Count = count
            });
        }
        await Console.Out.WriteLineAsync("SENDING REQUEST...\n\n");

        var res = await http.PostAsJsonAsync("/api/sale", req);
        var message = await res.Content.ReadAsStringAsync();

        await Console.Out.WriteLineAsync($"Response: {res.StatusCode} {message}");
    }
    catch (Exception ex)
    {
        await Console.Out.WriteLineAsync("Error");
        await Console.Out.WriteLineAsync(ex.Message);
    }
}

async Task UpdateSale()
{
    try
    {
        await Console.Out.WriteLineAsync("UPDATE SALE");
        await Console.Out.WriteAsync("Enter sale ID: ");
        var editedIdStr = await Console.In.ReadLineAsync();
        var editedId = int.Parse(editedIdStr!);

        await Console.Out.WriteAsync("Enter client name: ");
        var name = await Console.In.ReadLineAsync();

        var req = new CreateSaleRequest();
        req.ClientName = name!;
        req.SaleUnits = new List<SaleUnitDTO>();

        await Console.Out.WriteLineAsync("\n\nAdd products:");
        await Console.Out.WriteLineAsync("q\t Stop adding");
        bool addingUnits = true;
        while (addingUnits)
        {
            await Console.Out.WriteAsync("Product ID: ");
            var idStr = await Console.In.ReadLineAsync();
            if (!int.TryParse(idStr!, out int id)) break;

            await Console.Out.WriteAsync("Amount: ");
            var countStr = await Console.In.ReadLineAsync();
            if (!int.TryParse(countStr!, out int count)) break;

            req.SaleUnits.Add(new SaleUnitDTO()
            {
                WaterId = id,
                Count = count
            });
        }
        await Console.Out.WriteLineAsync("SENDING REQUEST...\n\n");

        var res = await http.PutAsJsonAsync($"/api/sale/{editedId}", req);
        var message = await res.Content.ReadAsStringAsync();

        await Console.Out.WriteLineAsync($"Response: {res.StatusCode} {message}");
    }
    catch (Exception ex)
    {
        await Console.Out.WriteLineAsync("Error");
        await Console.Out.WriteLineAsync(ex.Message);
    }
}
