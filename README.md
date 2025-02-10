---
author: Lektion 5
date: MMMM dd, YYYY
paging: "%d / %d"
---

# Lektion 5

Hej och välkommen!

## Dagens agenda

1. Frågor och repetition
2. Introduktion till databaser
3. Introduktion till Entity Framework
4. Övning med handledning

---

# Docker

Docker är ett program som hanterar andra program. Vi använder det för att installera och hantera databaser.

**Container**: program som körs i Docker

**Image**: mall för containers

Installera WSL (för Windows): <https://learn.microsoft.com/en-us/windows/wsl/install>

Installera Docker Desktop: <https://docs.docker.com/desktop/>

---

## Installera PostgreSQL

1. `docker pull postgres`
2. `docker run -p 5432:5432 -e POSTGRES_PASSWORD=password --name my-postgres -d postgres`
3. `docker start my-postgres` - om container är stoppad

## Gå in i PostgreSQL

1. `docker exec -it my-postgres bash` - gå in i container
2. `psql -U postgres` - starta klient och ange lösenord
3. `\l` - lista upp databaser
4. `CREATE DATABASE mydb;` - skapa egen databas
5. `\c mydb` - anslut till egen databas

---

# Introduktion till databaser

## Vad är en databas?

- Program som sparar och hanterar data
  - Exempel: uppgifter, transaktioner, blogginlägg, produkter, kommentarer
- Sparar data "persistently": den finns kvar även efter omstart
- Innehåller funktionalitet för avancerad sökning
  - Villkor
  - Funktioner

---

# Varför och varför inte listor?

**Fördelar**:

- Sparar data effektivt
- Snabb åtkomst

**Nackdelar**:

- Sparar data temporärt: den finns inte kvar efter omstart
- Låg lagringskapacitet: datorn har lite minne

---

# Varför och varför inte filer?

**Fördelar**:

- Sparar data persistently
- Hög lagringskapacitet: datorn har stort lager

**Nackdelar**:

- Sparar data oeffektivt
- Kan inte hantera sökning (måste implementeras manuellt med kod)
- Sparar lokalt: andra datorer kan inte se dem

---

# Exempel på databaser

- [PostgreSQL](https://www.postgresql.org/)
  - Anpassed för komplexa strukturer och relationer
- [MySQL](https://www.mysql.com/)
  - Anpassad för komplexa strukturer och relationer
  - Inte lika modern och effektiv som PostgreSQL
- [MongoDB](https://www.mongodb.com/)
  - Anpassad för simpla strukturer och få relationer
- [Redis](https://redis.io/)
  - Anpassad för simpla strukturer och få relationer
  - Extremt snabb
  - Låg lagringskapacitet

---

# Vilka databaser används i vilka situationer?

## SQL

- E-handel
- Sociala medier
- Banker

## NoSQL / Document

- Profil data
- Nyhetssida
- IoT (e.g. sensorer)

## In-memory

- Leaderboards & scoreboards
- Trading app

---

# Hur PostgreSQL fungerar

- Tabeller
- Rader
- Kolumner
- Constraints
- Relationer
- Joins
- Transactions
- SQL / queries

---

# PostgreSQL SQL exempel

```sql
-- Create a table
CREATE TABLE users (
  id SERIAL PRIMARY KEY,
  name VARCHAR(50) NOT NULL,
  email VARCHAR(100) UNIQUE NOT NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Insert data into a table
INSERT INTO users (name, email)
VALUES ('Ironman', 'tony@stark.com'),
       ('Superman', 'clark@kent.com'),
       ('Batman', 'bruce@wayne.com');

-- Query data from a table
SELECT * FROM users;
SELECT name, email FROM users WHERE id = 2;
SELECT COUNT(*) FROM users WHERE created_at > '2023-01-01';
```

---

# PostgreSQL kod exempel

```csharp
using var conn = new NpgsqlConnection("Host=localhost;Database=mydb;Username=myuser;Password=mypassword");
await conn.OpenAsync();

using var cmd = new NpgsqlCommand(@"
  CREATE TABLE users (name VARCHAR(50) NOT NULL, email VARCHAR(100) UNIQUE NOT NULL)", conn);
await cmd.ExecuteNonQueryAsync();

using var cmd = new NpgsqlCommand(@"INSERT INTO users (name, email) VALUES (@name, @email)", conn);
cmd.Parameters.AddWithValue("@name", "Ironman");
cmd.Parameters.AddWithValue("@email", "tony@stark.com");
await cmd.ExecuteNonQueryAsync();

using var cmd = new NpgsqlCommand("SELECT * FROM users", conn);
using var reader = await cmd.ExecuteReaderAsync();
while (await reader.ReadAsync()) {
    Console.WriteLine($"ID: {reader.GetInt32(0)}, Name: {reader.GetString(1)}, Email: {reader.GetString(2)}");
}
```

---

# Tabeller

En slags lista som håller information. Består av rader och kolumner.

# Rader

En samling med information likt ett Java objekt.

# Kolumner

Ett fält som finns i en rad. Likt fält (class fields) i Java klasser.

---

# Exempel tabeller

| Id | Name            | Email               | Created At          |
| -- | --------------- | ------------------- | ------------------- |
| 1  | Captain America | steve @avengers.com | 2015-05-01 12:00:00 |
| 2  | Ironman         | tony @avengers.com  | 2008-04-30 09:18:00 |
| 3  | Thor            | thor @asgard.com    | 2011-05-06 15:30:00 |
| 4  | Black Widow     | natasha @shield.gov | 2010-06-07 08:00:00 |
| 5  | Hulk            | bruce @gamma.lab    | 2012-03-15 11:45:00 |

| Id | Wizard Name        | Spell                   | Wand Type                 | Enrollment Date |
| -- | ------------------ | ----------------------- | ------------------------- | --------------- |
| 1  | Harry Potter       | Expecto Patronum        | Holly, Phoenix Feather    | 1991-09-01      |
| 2  | Hermione Granger   | Wingardium Leviosa      | Vine, Dragon Heartstring  | 1991-09-01      |
| 3  | Ron Weasley        | Arachnophobia           | Willow, Unicorn Hair      | 1991-09-01      |
| 4  | Albus Dumbledore   | Transfiguration         | Elder, Thestral Tail Hair | 1892-09-01      |
| 5  | Minerva McGonagall | Animagus Transformation | Fir, Dragon Heartstring   | 1947-09-01      |

---

# Begrepp och förkortningar

## Databas

Ett program som är till för att hantera stora mängder med data.

---

# Begrepp och förkortningar

## Relationsdatabas

En typ av databas som hanterar komplexa objekt genom relationer.

Data är strukturerad och anpassad för effektiv sökning.

Data är säker och pålitlig.

---

# Begrepp och förkortningar

## SQL (Structured Query Language)

Ett språk som vissa databaser använder för kommunikation. Det kan exempelvis användas för att skapa en "sökning".

---

# Begrepp och förkortningar

## Tabell

En strukturerad samling med rader i en SQL databas. De kan jämföras med listor (med objekt) i Java.

---

# Begrepp och förkortningar

## Model och Entity

En mall på en strukturerad rad i en tabell. De kan jämföras med klasser i Java.

Man kallar även ibland själva raderna för models.

---

# Begrepp och förkortningar

## Schema

Strukturen på en tabell. De definierar vilka kolumner, datatyper och constraints som skall finnas.

---

# Begrepp och förkortningar

## Rad / row

En samling med strukturerad data i en tabell. De kan jämföras med objekt i Java.

## Kolumn / column

En egenskap/fält/variabel för en rad i en tabell. De kan jämföras med fält i Java klasser. Varje kolumn har en datatyp.

---

# Begrepp och förkortningar

## ACID

1. **A**tomicity
2. **C**onsistency
3. **I**solation
4. **D**urability

Principer som man följer i databas design.

---

# Begrepp och förkortningar

## CRUD

1. **C**reate
2. **R**ead
3. **U**pdate
4. **D**elete

Vanliga handlingar som utförs inom databas sammanhang.

---

# Begrepp och förkortningar

## INSERT, SELECT, UPDATE, DELETE

CRUD nyckelord för SQL.

- **INSERT** (create): ladda upp data
- **SELECT** (read): hämta/sök data
- **UPDATE** (update): ändra befintlig data
- **DELETE** (delete): radera data

---

# Begrepp och förkortningar

## Query

Ett meddelande som skickas till en databas för att söka information. En "sökning".

Vissa kallar all SQL kod för queries, inklusive kod som sparar och raderar data.

---

# Begrepp och förkortningar

## Primary key och foreign key

Två speciella typer av kolumner (fält). Primary key är en identifierande kolumn. Foreign key är en refererande kolumn.

---

# Begrepp och förkortningar

## Relation

En koppling som finns mellan två tabeller, eller mer specifikt, två rader i två tabeller.

Exempel:

- Användare > Email
- Ordrar > Produkter
- Studerande > Kurser

---

# Begrepp och förkortningar

## Docker

Ett program som används för att hantera andra program. Det kan jämföras med Steam: Steam är ett spelbibliotek bland annat, och om man säger att ett spel är ett program så är Steam ett program av program. På samma sätt är Docker ett program av (andra) program.

Docker används för att installera (och avinstallera) program, köra dem, konfigurera dem och mer.

Vi använder Docker för att installera och hantera databaser.

---

# Begrepp och förkortningar

Språk för att kommunicera inom olika områden.

## DDL (Data Definition Language)

- Skapa schemas och tabeller
- Ta bort tabeller
- Modifiera tabeller

## DQL (Data Query Language)

- Hämta data (SELECT)

## DML (Data Manipulation Language)

- Ladda upp data (INSERT)
- Uppdatera data (UPDATE)

## DCL (Data Control Language)

- Hantera tillåtelser och användare

---

# Constraints

Extra krav, begränsningar och funktionalitet som kan appliceras på kolumner.

- `NOT NULL`
  - Värde kan inte vara null och måste få ett värde vid insert
- `UNIQUE`
  - Värde måste vara unikt och får inte finnas (för kolumnen) i tabell redan
- `CHECK`
  - Ser till att värde stämmer med vissa villkor
- `DEFAULT`
  - Kolumn får "default" värde om inget specificeras
- `PRIMARY KEY`
  - Identifierande kolumn, ofta `serial` och `uuid`
- `REFERENCES` (FOREIGN KEY)
  - Refererande kolumn (till en primary key)
- `SERIAL`
  - Tekniskt sätt inte en constraint, men har extra funktionalitet
  - `BIGINT` datatyp i bakgrunden
  - Ökar automatiskt (increment)

---

# Constraints exempel

```sql
CREATE TABLE user_accounts (
  id SERIAL PRIMARY KEY,
  username TEXT UNIQUE NOT NULL,
  email TEXT UNIQUE NOT NULL,
  password TEXT NOT NULL,
  age INT CHECK (age >= 18 AND age <= 120),
  created_at TIMESTAMP DEFAULT current_timestamp,
  online BOOL DEFAULT false,
  manager_id INT REFERENCES user_accounts(id) DEFAULT NULL
);
```

---

# SQL Funktioner

- Matematiska funktioner
- Sträng funktioner
- Tid och datum funktioner
- Aggregate funktioner
- GROUP BY, ORDER BY, HAVING

---

# Matematiska funktioner

- Enkla (+, -, *, /)
- Bitwise (AND, NOT, XOR, SHIFT)
- Funktioner (abs, exp, floor, log, sqrt)

<https://www.postgresql.org/docs/current/functions-math.html>

---

# Exempel på matematiska funktioner

```sql
CREATE TABLE products (price DECIMAL(10, 2));

SELECT price * 1.08 - 5 FROM products;
SELECT sqrt(price) FROM products;
SELECT abs(-price * 50) FROM products;
```

---

# Sträng funktioner

- Concatenation (||)
- Längd på sträng (char_length)
- Omvandla små eller stora bokstäver (lower & upper)
- Substring
- Trim (ltrim, btrim)

Många av dessa funktioner kan och bör göras med kod istället.

<https://www.postgresql.org/docs/current/functions-string.html>

---

# Exempel på sträng funktioner

```sql
CREATE TABLE employees (
    name VARCHAR(255),
    place VARCHAR(255)
);

SELECT 'Welcome to ' || place || ', ' || name || '.' FROM employees;
SELECT place FROM employees WHERE char_length(name) > 3;
SELECT upper(name) FROM employees;
```

---

# Datum och tid funktioner

- Addition och subtraktion (dagar, intervaller m.m)
- Ålder (age)
- Nuvarande tid (current_time, current_date)
- Truncate (date_trunc)
- Extrahera (extract)

<https://www.postgresql.org/docs/current/functions-datetime.html>

---

# Exempel på datum funktioner

```sql
CREATE TABLE appointments (
    start_date DATE,
    end_date DATE
);

INSERT INTO appointments (start_date, end_date) VALUES (current_date, '2020-05-14');
SELECT * FROM appointments WHERE start_date - 30 > '2024-10-01';
SELECT EXTRACT(DAY FROM start_date) FROM appointments WHERE end_date BETWEEN '2019-12-31' AND '2020-01-31';
```

---

# Aggregate funktioner

- Sum
- Count
- Min & max
- Avg

<https://www.postgresql.org/docs/current/functions-aggregate.html>

---

# Exempel på aggregate funktioner

```sql
CREATE TABLE salaries (
    employee_name TEXT,
    salary DECIMAL(10, 2),
    department VARCHAR(255)
);

SELECT avg(salary) FROM salaries;
SELECT count(*) FROM salaries;
SELECT sum(salary) FROM salaries;
```

---

# Andra funktioner

```sql
-- GROUP BY: Lägg alla med samma kolumner i en grupp
SELECT department, avg(salary) FROM salaries GROUP BY department;

-- ORDER BY: Sortera baserat på kolumn
SELECT employee_name, salary FROM salaries ORDER BY salary DESC;

-- HAVING: Inkludera endast grupper som matchar villkor
SELECT department, avg(salary) FROM salaries GROUP BY department HAVING avg(salary) > 30000;
```

<https://www.postgresql.org/docs/current/queries-table-expressions.html#QUERIES-GROUP>

---

# Introduktion till relationer

## Hur sparas listor av saker i SQL-databaser?

En kolumn kan endast spara ett värde och inte flera som en array. Istället hanteras listor av saker med "relationer".

En relation bildas när en rad i en tabell har en länk till en annan rad, oftast i en annan tabell.

---

# Exempel relation: Människor <-> Husdjur

```md
humans:

| name   | age |
| ------ | --- |
| Bob    | 30  |
| Rachel | 27  |

pets:

| name    | owner  |
| ------- | ------ |
| Charlie | Bob    |
| Oskar   | Bob    |
| Nevad   | Rachel |
```

---

# Primary keys och foreign keys

En relation är en länk mellan två rader. För att skapa en länk så används primary keys och foreign keys.

**Primary key**: en identifierande kolumn. Den är självstående och är typiskt sätt en `SERIAL` eller `UUID`.

**Foreign key**: en refererande kolumn. Refererar till en primary key.

För att referera till en primary key har foreign keys alltid samma värde som en primary key. En primary key kan dock vara helt unik, och utan foreign key.

I exemplet med människor och husdjur så är `humans:name` en primary key och `pets:owner` en foreign key. Detta är dock något som skaparen av tabellerna har bestämt.

---

# Typer av relationer

## One-to-One

En rad refererar till exakt en rad. Förekommer sällan.

## One-to-Many och Many-to-One

En rad refererar till flera (0-n) rader. One-to-Many och Many-to-One är samma sak från olika perspektiv.

Exempel: människor <-> husdjur, användare <-> kommentarer

## Many-to-Many

Flera rader refererar till flera rader. Om One-to-Many går åt båda hållen så är det Many-to-Many. Skapas med "junction" tabeller.

Exempel: användare <-> git repos, spelare <-> spel

---

# Introduktion till ORMs

Object-relational mapping, förkortat ORM, är ett verktyg som utvecklare kan använda för att förenkla databashantering.

- Skapar tabeller (CREATE TABLE)
- Automatiserar queries (SELECT, INSERT, DELETE)
- Förenklar villkor och joins
- Hanterar versioner av tabeller

---

# Entity Framework (EF)

Entity Framework är en ORM för C# som har stöd för det mesta.

- Klasser bildar modeller som bildar tabeller
- Migrations för att uppdatera tabeller
- Funktioner för CRUD
- Stöd för relationer och joins

```sh
# Installera och kom igång med EF i ett projekt
dotnet tool install --global dotnet-ef
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
```

<https://learn.microsoft.com/en-us/ef/core/get-started/overview/install>

---

# DbContext

- Ansluter till databas
- Kommunicerar med databas (queries)
- Hanterar relationer och joins automatiskt

---

# Exempel DbContext

```csharp
public class Todo // Model
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
}

public class AppContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql("Host=localhost;Database=todos;Username=postgres;Password=password");
    }
}
```

---

# Migrations

- Beskrivning av tabeller
- Håller koll på historik och versioner
- Skapar och uppdaterar tabeller

```sh
# Skapa en migration och uppdatera databasen
dotnet ef migrations add [namn]
dotnet ef database update
```

---

# Ett enkelt exempel

```csharp
using var db = new AppContext();

var todo = new Todo { Title = "Städa" };

// INSERT: Spara todo
db.Todos.Add(todo);
db.SaveChanges();

// DELETE: Radera todo
db.Todos.Remove(todo);
db.SaveChanges();

// SELECT: Hämta todos
var todos = db.Todos.Where(todo => todo.Title.Contains("a"));
foreach (var todo in todos)
{
    Console.WriteLine(todo.Title);
}
```
