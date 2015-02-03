# What is Radish?

Radish is a simple .Net library that allows you to auto-generate HTML-documentation for your REST API on the basis of attributes in the source code.
Radish is an acronym that means ***R***EST ***A***PI ***D***ocumentation ***I***n the ***S***ource to ***H***tml.

Advantages of this approach are:
- you do not need to create REST API documentation manually;
- you do not need to sync your documentation file with changes in the code, as it needed for other popular generators, like [Swagger] or [ApiBlueprint];
- unlike the [ASP.NET Web API Help Page] solution, Radish does not require [WebHost], [Mvc] and [Razor] libraries, and suits for small self-hosting RESTful servers;
- all documentation is a single HTML-page with CSS and JavaScript embedded.

From the source code point of view, writing documentation with Radish looks like:

```csharp
#region Radish
[Radish.Route("POST", "/api/cats")]
[Radish.Title("Add new cat")]
[Radish.Description("Adds new cat to the repository")]
[Radish.ResponseCode("200", "Ok. Cat was added")]
[Radish.ResponseCode("400", "Bad request. Incorrect Json data were passed in the message body")]
[Radish.ResponseCode("500", "Internal server error. See exception message for details")]
#endregion Radish
[HttpPost]
[Route("cats")]
[ResponseType(typeof(Cat))]
public IHttpActionResult AddRouter([FromBody] Cat cat) 
{
	if (cat == null) 
	{
		return BadRequest("Incorrect JSON data were passed.");
	}
	try 
	{
		Cat newCat = repository.AddCat(cat);
		return Created<Cat>(new Uri(Request.RequestUri, newCat.Id.ToString()), newCat);
	}
	catch (Exception ex) 
	{
		return InternalServerError(ex);
	}
}
```

Radish uses own simple template engine to create HTML documentation page, and you can freely customize its output.
Let's take a look on the [output documentation example] for [this simple demo].
Check the [guide] how to use Radish and customize your documentation page.

# License
Radish is licensed under Apache 2.0 license.  

[Swagger]:http://swagger.io/
[ApiBlueprint]:https://apiblueprint.org/
[ASP.NET Web API Help Page]:http://www.nuget.org/packages/Microsoft.AspNet.WebApi.HelpPage
[WebHost]:http://www.nuget.org/packages/Microsoft.AspNet.WebApi.WebHost/
[Mvc]:http://www.nuget.org/packages/Microsoft.AspNet.Mvc/
[Razor]:http://www.nuget.org/packages/Microsoft.AspNet.Razor/
