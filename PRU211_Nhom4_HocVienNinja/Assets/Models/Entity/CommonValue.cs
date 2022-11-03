using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonValue : MonoBehaviour
{
    public static CharacterEntity CharacterSelected;

    public static List<SkillEntity> Skill;

    public static List<EnemyEntity> Enemy;

    public static int ItemOneQuantity;

    public static int ItemTwoQuantity;

    CharacterDAO characterDAO;
    SkillDAO skillDAO;
    EnemyDAO enemyDAO;
    AccountDAO accountDAO;
    private void Awake()
    {
        characterDAO = GetComponent<CharacterDAO>();
        skillDAO = GetComponent<SkillDAO>();
        enemyDAO = GetComponent<EnemyDAO>();
        accountDAO = GetComponent<AccountDAO>();
        CharacterSelected = characterDAO.GetCharacterbyID(1);
        Skill = skillDAO.GetAllSkill();
        Enemy = enemyDAO.GetAllEnemy();
        ItemOneQuantity = accountDAO.GetItemQuantity(AccountManager.AccountID, 1);
        ItemTwoQuantity = accountDAO.GetItemQuantity(AccountManager.AccountID, 2);
    }


}
