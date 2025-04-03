# 👻 Ghostchef – Måltidskasse Abonnementssystem  
**Datamatiker Eksamen – Forår 2024**

Ghostchef er en webapplikation udviklet i C# med Razor Pages og Entity Framework. Formålet er at tilbyde brugere mulighed for at oprette og tilpasse abonnementer på måltidskasser, administrere kostpræferencer og håndtere betalinger og fakturaer – alt sammen som en udvidelse til Ghostchefs eksisterende website.

## 🧩 Funktionalitet

- 🔐 Brugeroprettelse og login (rollebaseret adgang)
- 📦 Oprettelse og administration af måltidskasser (MealBoxes)
- 🍽️ Tilpasning af måltidskasser med produkter og filtrering på kost/allergi
- 📋 Abonnementsstyring (leveringsfrekvens og tilknytning af kasser)
- 💳 Faktura- og betalingshåndtering
- 🧑‍🍳 Administratorinterface til redigering af måltidskasser og produkter

## 🛠️ Teknologi-stack

| Lag            | Teknologi                     |
|----------------|-------------------------------|
| Frontend       | Razor Pages (.cshtml)         |
| Styling        | Bootstrap 5, CSS              |
| Backend        | ASP.NET Core 6 (C#)           |
| Database       | MS SQL Server                 |
| ORM            | Entity Framework Core         |
| Hosting        | Lokal IIS / Azure-kompatibel  |
| Versionsstyring| GitHub                        |

## 🗄️ Datamodel

Relationel database i 3NF med følgende centrale tabeller:

- `User`: Roller (`Customer`, `Admin`), loginoplysninger  
- `Customer`: Kontaktoplysninger og byrelation  
- `Subscription`: Abonnement med leveringsfrekvens og kobling til `MealBox`  
- `MealBox`: Måltidskasse med tilhørende `Product`-elementer og gyldighedsperiode  
- `Product`: Retter med kobling til `Diet` og `Allergy`  
- `Invoice` + `Charge`: Fakturering og betalingshistorik  
- Mellemtabeller: `MealboxProduct`, `ProductDiet`, `SubscriptionMealbox` m.fl.

## 🚀 Kom i gang

1. Klon projektet  
   `git clone https://github.com/Hillgrove/Ghostchef.git`

2. Opsæt database  
   Kør `Update-Database` i Visual Studio eller via CLI:  
   `dotnet ef database update`

3. Start backend  
   Åbn `.sln` og kør via IIS Express eller Kestrel

## 👤 Brugerroller

| Rolle     | Rettigheder                                   |
|-----------|-----------------------------------------------|
| Customer  | Se/tilpas egne abonnementer og måltidskasser  |
| Admin     | CRUD på produkter, måltidskasser og brugere   |

## ✅ Implementeret i kode

- CRUD på `Product`, `MealBox`, `Subscription`, `Invoice`, `Charge`
- Razor Pages med `OnGet`, `OnPost` og inputvalidering
- Sessionsbaseret login og rollebaseret adgang
- Filtrering af produkter på allergi og kost
- Dynamiske lister og relationer i visning og oprettelse
- Automatiske relationer ved brugeroprettelse

## ⚠️ Kendte begrænsninger

- Ingen rigtig betaling – fakturaer er fiktive
- Ingen email-notifikationer
- Begrænset mobilvenlighed (desktop-optimeret)

## 📈 Videreudvikling

- Integration med betalingsgateway
- Mobilvenlig frontend og bedre styling
- Automatisk gentegning af abonnementer
- PDF-faktura og download
- Bedre audit-log og brugerhåndtering
