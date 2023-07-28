using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //폴더 안에 저장 된 파일을 확인 -> 존재 유무 파악, 파일 불러올 때 필요한 시스템
using System;

//https://chameleonstudio.tistory.com/56
public class DataController : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if(!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
