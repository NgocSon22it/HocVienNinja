using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

public class EnemyDAO : MonoBehaviour
{
    string ConnectionStr = new HocVienNinjaConnect().GetConnectHocVienNinja();

    public EnemyEntity GetEnemybyID(int id)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Enemy where Ene_ID = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    EnemyEntity a = new()
                    {
                        EnemyID = Convert.ToInt32(dr["Ene_ID"]),
                        EnemyName = dr["Name"].ToString(),
                        TotalHealthPoint = Convert.ToInt32(dr["Health"]),
                        EnemyDamage = Convert.ToInt32(dr["Damage"]),
                        EnemySpeed = Convert.ToInt32(dr["Speed"]),
                        EnemyCoin = Convert.ToInt32(dr["Coin"]),
                        Description = dr["Description"].ToString(),
                        Link = dr["Link"].ToString(),
                        Delete = Convert.ToBoolean(dr["Delete"])
                    };
                    connection.Close();
                    return a;
                }
            }
            finally
            {
                connection.Close();
            }

        }

        return null;
    }
}
