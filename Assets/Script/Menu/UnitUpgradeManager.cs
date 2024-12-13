using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitUpgradeManager : MonoBehaviour
{
    public GameObject[] UpgradeMenuUnits;

    private string currentPressedButtonName;
    private string prevPressedButtonName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �Ȃɂ��{�^���������ꂽ�炻��ɑΉ�����������ʂ�\��
        if(GetLastPressedButton() != null)
        {
            // �����ꂽ���j�b�g�̃{�^�������擾���A�\�����鋭����ʂ̖��O�ɕς���
            prevPressedButtonName = currentPressedButtonName;
            currentPressedButtonName = GetLastPressedButton().name;
            string buttonName = "";
            Debug.Log("currentPressedButtonName��[" + currentPressedButtonName + "]�ł�");
            Debug.Log("prevPressedButtonName[" + prevPressedButtonName + "]�ł�");

            // �����ꂽ�{�^���̖��O��"Button"���܂܂�Ă����炻��ɂ�����������ʂ�\������
            if(currentPressedButtonName.Contains("Button"))
            {
                Debug.Log("���j�b�g�I���{�^��������Ă܂�");

                buttonName = currentPressedButtonName.Split("_")[0];
                buttonName += "_upgrade";
                Debug.Log("buttonName" + buttonName + "�ɕύX����܂���");

                GameObject upgradeMenu = FindUpgradeMenu(buttonName);
                if (upgradeMenu)
                {
                    upgradeMenu.SetActive(true);
                }
            }

            // �����{�^���������ꂽ��X�e�[�^�X����
            if(currentPressedButtonName.Contains("upgrade_button") && prevPressedButtonName.Contains("Button"))
            {
                Debug.Log("�����{�^��������Ă܂�");

                buttonName = prevPressedButtonName.Split("_")[0];
                buttonName += "_upgrade";
                Debug.Log("buttonName" + buttonName + "�ɕύX����܂���");

                Debug.Log(buttonName + "�Ō������܂�");
                GameObject upgradeMenu = FindUpgradeMenu(buttonName);
                Debug.Log(upgradeMenu.name + "�̏����Ă܂�");
                UpgradeUnitStore upgradeUnit;
                if(upgradeMenu.TryGetComponent<UpgradeUnitStore>(out upgradeUnit))
                {
                    DefenseUnit_Base defenUnit;
                    if(upgradeUnit.unit.TryGetComponent<DefenseUnit_Base>(out defenUnit))
                    {
                        // �t���O�ɂ���ċ�������X�e�[�^�X��ύX
                        if(upgradeUnit.UpgradeAtkPower)
                        {
                            defenUnit.AddAttackPower(upgradeUnit.UpgradeAmount);
                        }
                        else if(upgradeUnit.UpgradeAtkRange)
                        {
                            defenUnit.AddAttackRange(upgradeUnit.UpgradeAmount);
                        }
                        else if(upgradeUnit.UpgradeLifeTime)
                        {
                            defenUnit.AddLifeTime(upgradeUnit.UpgradeAmount);
                        }
                    }
                    else
                    {
                        Debug.LogError(upgradeUnit.unit.name + "��DefenceUnit_Base���p������Ă��܂���");
                    }
                }
                else
                {
                    Debug.LogError("UpgradePanel->"+ buttonName +"->UpgradeUnitStore�Ƀ��j�b�g������܂���");
                }

            }


        }
    }

    // �Ō�ɉ����ꂽ�{�^�����擾
    GameObject GetLastPressedButton()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        GameObject lastClick = null;

        if(Input.GetMouseButtonUp(0))
        {
            // Raycast�̌��ʂ��i�[���郊�X�g
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // ���X�g����łȂ��ꍇ�A�Ō�ɃN���b�N���ꂽGameObject���擾���AButton�R���|�[�l���g���擾
            if (results.Count > 0)
            {
                lastClick = results[results.Count - 1].gameObject;
            }
        }


        return lastClick;
    }

    // �����Ŏw�肳�ꂽ���O�̋�����ʂ����X�g���ɂ��邩�ǂ����𔻒肵�A���X�g���ɂ���΂����Ԃ�
    GameObject FindUpgradeMenu(string unitName)
    {
        foreach(GameObject upgradeMenu in UpgradeMenuUnits)
        {
            if(upgradeMenu.name == unitName)
            {
                return upgradeMenu;
            }
        }

        return null;  
    }
}

