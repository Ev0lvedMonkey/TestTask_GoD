public class HelmetArmorSlot : ArmorSlot
{
    protected override void EquipUniqArmor()
    {
        base.EquipUniqArmor();
        _character.EquipHeadArmor(new Armor(_armorItem.Defense));
    }

    protected override void UnequipUniqArmor()
    {
        _character.UnequipHeadArmor();
    }
}

