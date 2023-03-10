using PizzaAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.SqlClient;
using Dapper;

namespace PizzaAPI.Models;

public class BD{
private static string _connectionString = @"Persist Security Info=False;User ID=Pizzas;password=Pizzas;Initial Catalog=DAI-Pizzas;Data Source=.;";

public static List<Pizza> BuscarPizzas(){
    List<Pizza> lista = new List<Pizza>();
    using(SqlConnection db = new SqlConnection(_connectionString)){
        string sql = "SELECT * FROM Pizzas";
        lista = db.Query<Pizza>(sql).ToList();
    }
    return lista;
}

public static Pizza BuscarPizzaPorId(int id){
    List<Pizza> pizza = new List<Pizza>();
    using(SqlConnection db = new SqlConnection(_connectionString)){
        string sql = "SELECT * FROM Pizzas WHERE Id = @pid";
        pizza = db.Query<Pizza>(sql, new{pid = id}).ToList();
    }
    return pizza[0];
}

public static void CrearPizzas(Pizza p)
{
    string sql = "INSERT INTO Pizzas VALUES (@pNombre, @pLibregluten, @pImporte, @pDescripcion)";
    using(SqlConnection db = new SqlConnection(_connectionString)){
        db.Execute(sql, new {pNombre = p.Id, pLibreGluten = p.LibreGluten, pImporte = p.Importe, pDescripcion = p.Descripcion});
    }
}

public static void ActualizarPizza(int id, Pizza p)
{
    string sql = "UPDATE Pizzas SET Nombre = @pNombre, LibreGluten = @pLibregluten, Importe = @pImporte, Descripcion = @pDescripcion WHERE Id = @pid";
    using(SqlConnection db = new SqlConnection(_connectionString)){
        db.Execute(sql, new {pNombre = p.Id, pLibreGluten = p.LibreGluten, pImporte = p.Importe, pDescripcion = p.Descripcion, pid = id});
    }
}

public static void BorrarPizzas(int id)
{
    string sql = "DELETE FROM Pizzas WHERE Id = @pid";
    using(SqlConnection db = new SqlConnection(_connectionString)){
        db.Execute(sql, new{pid = id});
    }
}
}