﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class NewspaperCloseButtonController : MonoBehaviour {

    private Game game;
    NewspaperController newspaperController;


    void Awake()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }
    
    void Start () {
        newspaperController = gameObject.transform.parent.GetComponent<NewspaperController>();
    }

    void OnMouseUp() {
        newspaperController.Close();
        game.ClearCurrentHouse();
    }
}
