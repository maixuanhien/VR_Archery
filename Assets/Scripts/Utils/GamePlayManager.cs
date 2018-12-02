using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager> {

    [SerializeField]
    private Transform player;
    [SerializeField]
    private List<GameObject> listArrow;
    private int score = 0;

    private void Start() {
        if(player == null) {
            player = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).transform;
        }
    }

    public Transform GetPlayerTransform() {
        return player;
    }

    public int GetHP(string tag) {
        if (tag == Constants.TAG_MONSTER_AKASHITA) {
            return Constants.HP_MONSTER_AKASHITA;
        } else if (tag == Constants.TAG_MONSTER_SESSHOSEKI) {
            return Constants.HP_MONSTER_SESSHOSEKI;
        } else if (tag == Constants.TAG_MONSTER_BAKEZORI) {
            return Constants.HP_MONSTER_BAKEZORI;
        } else {
            return 0;
        }
    }

    public void AddScore(string tag) {
        if (tag == Constants.TAG_MONSTER_AKASHITA) {
            score = score + Constants.SCORE_MONSTER_AKASHITA;
        } else if (tag == Constants.TAG_MONSTER_SESSHOSEKI) {
            score = score + Constants.SCORE_MONSTER_SESSHOSEKI;
        } else if (tag == Constants.TAG_MONSTER_BAKEZORI) {
            score = score + Constants.SCORE_MONSTER_BAKEZORI;
        }
    }

    public int GetScore() {
        return score;
    }

    public int GetDiffCulty() {
        if(score < Constants.SCORE_MAX_LEVEL_1) {
            return 1;
        }else if(score < Constants.SCORE_MAX_LEVEL_2) {
            return 2;
        }else if(score < Constants.SCORE_MAX_LEVEL_3) {
            return 3;
        }else if(score < Constants.SCORE_MAX_LEVEL_4) {
            return 4;
        } else {
            return 0;
        }
    }

    public GameObject GetArrow() {
        if (listArrow.Count > 0) {
            int index = GetDiffCulty();
            if(index > listArrow.Count || index == 0) {
                index = listArrow.Count;
            }
            return listArrow[index - 1];
        } else {
            return null;
        }
    }
}
