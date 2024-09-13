var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.UseHttpsRedirection();

app.Run(async (HttpContext context) => {

    // Check if the request is a GET request and the path is "/"
    if (context.Request.Method != "GET" || context.Request.Path != "/")
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Error in type of request use GET request only or in localhost path");
    }

    // Check if all required query parameters exist
    if (!context.Request.Query.ContainsKey("firstNumber") || !context.Request.Query.ContainsKey("secondNumber") || !context.Request.Query.ContainsKey("operation"))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Some query/queries are missing. Check 'firstNumber', 'secondNumber', and 'operation' are provided.");
    }

    // Retrieve query parameters
    string firstNumber = context.Request.Query["firstNumber"].ToString();
    string secondNumber = context.Request.Query["secondNumber"].ToString();
    string operation = context.Request.Query["operation"].ToString();

    // Try parsing the numbers
    double f1;
    if (!double.TryParse(firstNumber, out f1))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid firstNumber value.");
    }

    double f2;
    if (!double.TryParse(secondNumber, out f2))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid secondNumber value.");
    }

    // Check for division by zero
    if (operation == "division" && f2 == 0)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Division by zero not Allowed");
    }

    // Perform the operation if valid
    if (operation == "division" || operation == "addition" || operation == "substraction" || operation == "remainder" || operation == "multiplication")
    {
        context.Response.StatusCode = 200;
        double ans = 0;
        switch (operation)
        {
            case "addition":
                ans = f1 + f2;
                break;

            case "substraction":
                ans = f1 - f2;
                break;

            case "multiplication":
                ans = f1 * f2;
                break;

            case "division":
                ans = f1 / f2;
                break;

            case "remainder":
                ans = f1 % f2;
                break;
        }

        // Return the result
        await context.Response.WriteAsync($"{ans}");
    }
    else
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid operation. Use 'addition', 'substraction', 'multiplication', 'division', or 'remainder'.");
    }

});

app.Run();
