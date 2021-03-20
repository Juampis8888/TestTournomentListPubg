using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTournaments : MonoBehaviour
{
    public AdapterIDAndDate AdapterIDAndDate;

    public ConsultTournaments ConsultTournaments;

    public RectTransform Content;

    void Update()
    {
        if (Content.childCount < 3)
        {
            Debug.Log("Init");
            int Count = 0;
            var Position = 0;
            var AwardsRectTransform = AdapterIDAndDate.GetComponent<RectTransform>();
            float templateHeight = AwardsRectTransform.rect.height;
            ConsultTournaments.RootTournoment.data.ForEach(tournament =>
            {
                Debug.Log(tournament.id);
                float top = (((Position * templateHeight)) + 300) * -1;
                var item = Instantiate(AdapterIDAndDate);
                item.gameObject.SetActive(true);
                item.name = "Tournament" + (Position + 1);
                item.Parent(Content);
                item.Location(top);
                item.Id.text = tournament.id;
                item.Date.text = tournament.attributes.createdAt;

                item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Position++;
                Count++;
            });
            var height = Count * templateHeight;
            Content.localPosition = new Vector3(Content.localPosition.x, 0, Content.localPosition.z);
            Content.sizeDelta = new Vector2(Content.rect.width, height);
        }
    }
}
