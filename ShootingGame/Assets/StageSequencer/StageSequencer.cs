using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

//System.IO �E�E�E System.InputOutput�̗�

[CreateAssetMenu(menuName = "StageSequencer")]
public class StageSequencer : ScriptableObject
{
    [SerializeField] private string fileName = "";

    public enum CommandType
    {
        SETSPEED,
        PUTENEMY
    }

    static readonly Dictionary<string, CommandType> commandlist
              = new Dictionary<string, CommandType>()
              {
                  { "SETSPEED",CommandType.SETSPEED },
                  { "PUTENEMY",CommandType.PUTENEMY },
              };

    public struct StageData
    {
        public readonly float eventPosition;
        public readonly CommandType command;
        public readonly float data1, data2;
        public readonly uint data3;

        public StageData(float eventPosition , string command , float x , float y , uint type)
        {
            this.eventPosition = eventPosition;
            this.command       = commandlist[command];//SETSPEED���APUTENEMY���Ԃ�
            data1              = x;
            data2              = y;
            data3              = type;
        }
    }
    
    StageData[] stageDatas;
    private int stageIndex = 0;

    [SerializeField] Enemy[] enemyPrefabs = null;

    public void Load()
    {
        var revarr = new Dictionary<string, uint>();
        for (uint i = 0; i < enemyPrefabs.Length; i++)
        {
            revarr.Add(enemyPrefabs[i].name, i);
        }

        var stageCSVData = new List<StageData>();
        var csvData = Resources.Load<TextAsset>(fileName).text;

        StringReader sr = new StringReader(csvData); //sr = stringreader

        while (sr.Peek() != -1)
        {
            var line = sr.ReadLine();
            var cols = line.Split(',');

            if (cols.Length != 5) continue;

         /*��l�����́A����CSV�ɏ����Ă���G�̖��O���Ԉ���Ă�����AenemyPrefabs��0�Ԗڂ��p���鏈��*/
            stageCSVData.Add(new StageData(float.Parse(cols[0]), cols[1],
                                           float.Parse(cols[2]), float.Parse(cols[3]),
                                           revarr.ContainsKey(cols[4]) ? revarr[cols[4]] : 0));
        }
        stageDatas = stageCSVData.OrderBy(item => item.eventPosition).ToArray();//�\�[�g���Ƃ�
    }


    public void StageReset()
    {
        stageIndex = 0;
    }

    /*�X�e�b�v���s���邽�߂̏��@�����Ԃ̌v����StageControl�Ōv�����Ă�B*/
    public void Step(float stageProgressTime)�@//stageProgressTime = �X�e�[�W�̐i�s����
    {
        while (stageIndex < stageDatas.Length && stageDatas[stageIndex].eventPosition <= stageProgressTime)
        {
            switch (stageDatas[stageIndex].command)
            {
                case CommandType.SETSPEED:
                    StageControl.Instance.stageSpeed = stageDatas[stageIndex].data1;
                    break;

                case CommandType.PUTENEMY:
                    var enemyTmp = Instantiate(enemyPrefabs[stageDatas[stageIndex].data3]);

                    enemyTmp.transform.parent = StageControl.Instance.enemys;
                    enemyTmp.transform.localPosition
                        = new(stageDatas[stageIndex].data1, 15, stageDatas[stageIndex].data2);

                    break;
            }
            stageIndex++;
        }
    }

}
