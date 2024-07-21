using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateObjective : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    private Dictionary<int, string> objectives = new Dictionary<int, string>();
    private HashSet<int> completedObjectives = new HashSet<int>();

    public void UpdateObjectiveText(string newObjective)
    {
        objectiveText.text = newObjective;
    }

    void Start()
    {
        // Inisialisasi teks objective awal
        UpdateObjectiveText("Find the Brosur!");

        // Tambahkan item ID dan teks objective yang sesuai ke dalam dictionary
        objectives.Add(1, "Explore inside Museum Betawi");
        objectives.Add(6, "Find the lost food");
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryManager.Instance != null)
        {
            // Periksa apakah pemain memiliki item dengan ID yang dibutuhkan
            foreach (Item item in InventoryManager.Instance.Items)
            {
                if (objectives.ContainsKey(item.id) && !completedObjectives.Contains(item.id))
                {
                    // Update objective jika item ditemukan
                    UpdateObjectiveText(objectives[item.id]);
                    completedObjectives.Add(item.id); // Tandai objective sebagai selesai
                    break; // Hentikan loop setelah menemukan item yang sesuai
                }
            }
        }
    }
}
