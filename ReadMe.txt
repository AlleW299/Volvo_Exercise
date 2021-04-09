Volvo_ctc
----------------------
Skrivet i VS 2019 Version 16.8.4, Använde mig av eran C# Core kod.
Endast minimala ändringar gjorde på den befintliga koden (fixade motorcycle / motorbike buggen tex)
Använder nuget paketen Json.net och Swashbuckle.AspNetCore för hanteringen av Json och Swagger

Öppna Volvo_CTS och dubbel-klicka på VOlvo_CTC.sln för att få upp båda projekten i VS Studio.

Alla unittest är gröna och debug av projektet öppnar Swagger sidan för testning av det apiet jag skrev för CongestionTaxCalculator.cs / GetTax

Om den pga någon anledning inte visar Swaggersidan vid körning, så finner du den här ... https://localhost:44328/swagger/index.html