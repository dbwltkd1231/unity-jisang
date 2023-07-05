using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
   public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialgoueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvData.text.Split(new char[]{'\n'});// ����Ű�������� �������ɰ���
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });
            Dialogue dialogue = new Dialogue();
            dialogue.name = row[1];//0��°�� ID

            List<string> contextlist = new List<string>();

            
            do
            {
                
                contextlist.Add(row[2]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else 
                {
                    break;
                }

            }
            while (row[0].ToString() == "");
            dialogue.contexts = contextlist.ToArray();
            dialgoueList.Add(dialogue);
            
        }
        
        return dialgoueList.ToArray();
    }

   
}
