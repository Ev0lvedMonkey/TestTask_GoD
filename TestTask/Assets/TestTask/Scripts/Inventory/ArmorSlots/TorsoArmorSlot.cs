public class TorsoArmorSlot : ArmorSlot
{
    protected override void EquipUniqArmor()
    {
        base.EquipUniqArmor();
        _character.EquipTorsoArmor(new Armor(_armorItem.Defense));
    }

    protected override void UnequipUniqArmor()
    {
        _character.UnequipTorsoArmor();
    }
}
