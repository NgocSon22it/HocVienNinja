using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

public class EnemyDAO : MonoBehaviour
{
    string ConnectionStr = new HocVienNinjaConnect().GetConnectHocVienNinja();

    public List<EnemyEntity> GetAllEnemy()
    {
        List<EnemyEntity> list = new List<EnemyEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Enemy";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new EnemyEntity
                    {
                        EnemyID = Convert.ToInt32(dr["Ene_ID"]),
                        EnemyName = dr["Name"].ToString(),
                        TotalHealthPoint = Convert.ToInt32(dr["Health"]),
                        EnemyDamage = Convert.ToInt32(dr["Damage"]),
                        EnemySpeed = Convert.ToInt32(dr["Speed"]),
                        EnemyCoin = Convert.ToInt32(dr["Coin"]),
                        Description = dr["Description"].ToString(),
                        Link = dr["Link"].ToString(),
                        Delete = Convert.ToBoolean(dr["Delete"].ToString())
                    });
                }

            }
            finally
            {
                connection.Close();

            }
        }

        return list;
    }
}
