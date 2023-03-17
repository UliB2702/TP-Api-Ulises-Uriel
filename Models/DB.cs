using PizzaAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.SqlClient;
using Dapper;

namespace PizzaAPI.Models;

public class BD{
private static string _connectionString = @"Server=A-PHZ2-CIDI-017;DataBase=DAI-Pizzas;Trusted_Connection=True";

public static List<Pizza> BuscarPizzas(){
    List<Pizza> lista = new List<Pizza>();
    using(SqlConnection db = new SqlConnection(_connectionString)){
        string sql = "SELECT * FROM Pizzas";
        lista = db.Query<Pizza>(sql).ToList();
    }
    return lista;
}

public static Pizza BuscarPizzaPorId(int id){
    Pizza pizza = null;
    using(SqlConnection db = new SqlConnection(_connectionString)){
        string sql = "SELECT * FROM Pizzas WHERE Id = @pid";
        pizza = db.QueryFirstOrDefault<Pizza>(sql, new{pid = id});
    }
    return pizza;
}

public static void CrearPizzas(Pizza p)
{
    string sql = "INSERT INTO Pizzas VALUES (@pNombre, @pLibregluten, @pImporte, @pDescripcion)";
    using(SqlConnection db = new SqlConnection(_connectionString)){
            db.Execute(sql, new {pNombre = p.Nombre, pLibreGluten = p.LibreGluten, pImporte = p.Importe, pDescripcion = p.Descripcion});
    }
}

public static int ActualizarPizza(int id, Pizza p)
{
    string sql = "UPDATE Pizzas SET Nombre = @pNombre, LibreGluten = @pLibregluten, Importe = @pImporte, Descripcion = @pDescripcion WHERE Id = @pid";
    int intRowsAffected = 0;
    using(SqlConnection db = new SqlConnection(_connectionString)){
        intRowsAffected = db.Execute(sql, new {pNombre = p.Nombre, pLibreGluten = p.LibreGluten, pImporte = p.Importe, pDescripcion = p.Descripcion, pid = id});
    }
    return intRowsAffected;
}

public static int BorrarPizzas(int id)
{
    string sql = "DELETE FROM Pizzas WHERE Id = @pid";
    int intRowsAffected = 0;
    using(SqlConnection db = new SqlConnection(_connectionString)){
        intRowsAffected = db.Execute(sql, new{pid = id});
    }
    return intRowsAffected;
}
}