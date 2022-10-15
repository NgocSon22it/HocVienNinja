using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

public class SkillDAO : MonoBehaviour
{
    string ConnectionStr = new HocVienNinjaConnect().GetConnectHocVienNinja();

    public SkillEntity GetSkillbyID(int id)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Skill where Skill_ID = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    SkillEntity a = new()
                    {
                        Name = dr["Name"].ToString(),
                        Chakra = Convert.ToInt32(dr["Chakra"]),
                        Damage = Convert.ToInt32(dr["Damage"]),
                        Cooldown = Convert.ToInt32(dr["Cooldown"]),
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
