namespace Acme.Dressing
{
    public delegate void DressCommandValidationEventHandler(object sender, DressCommandValidationEventArgs e, ref bool cancel);
}
