using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

//System.IO ・・・ System.InputOutputの略

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
                  { "PUTENEMM",CommandType.PUTENEMY },
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
            this.command       = commandlist[command];//SETSPEEDか、PUTENEMYが返る
            data1              = x;
            data2              = y;
            data3              = type;
        }
    }
    
    StageData[] stageDatas;
    private int stageIndex = 0;


    public void Load()
    {
        var stageCSVData = new List<StageData>();
        var csvData = Resources.Load<TextAsset>(fileName).text;

        StringReader sr = new StringReader(csvData); //sr = stringreader



        while (sr.Peek() != -1)
        {
            var line = sr.ReadLine();
            var cols = line.Split(' ');

            if (cols.Length != 5) continue;

            //第四引数は敵の種類を入れる予定。
            stageCSVData.Add(new StageData(float.Parse(cols[0]), cols[1],
                                           float.Parse(cols[2]), float.Parse(cols[3]),0));
        }
        stageDatas = stageCSVData.OrderBy(item => item.eventPosition).ToArray();
    }


    private void StageReset()
    {
        stageIndex = 0;
    }

    public void Step(float stageProgressTime)　//stageProgressTime = ステージの進行時間
    {
        while (stageIndex < stageDatas.Length && stageDatas[stageIndex].eventPosition <= stageProgressTime)
        {

        }
    }

}
