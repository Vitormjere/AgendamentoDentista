# Clínica Odontológica

A C# console application for dental clinic appointment management, featuring patient registration, procedure tracking, and appointment scheduling with MySQL database integration.
The system allows dental staff to register and manage patients, track available procedures, and schedule appointments, all stored in a MySQL database.

## Features
- Patient registration and management
- Procedure catalog with pricing
- Appointment scheduling linked to patients and procedures
- View full appointment history
- Filter appointments by date or patient
- Search and update patient records

## Technologies
- C#
- .NET 8
- MySQL
- MySql.Data (database connector)
- DotNetEnv (environment variables)

## Database Setup
Create a MySQL database called `clinica_odontologica` and run the SQL script located in `Database/schema.sql` to create the required tables.

## Configuration
Clone the repository, create a `.env` file in the project root based on `.env.example`, and fill in your database credentials:

DB_HOST=localhost
DB_PORT=3306
DB_NAME=clinica_odontologica
DB_USERNAME=root
DB_PASSWORD=

## Author
Vitor Miranda Jeremias
