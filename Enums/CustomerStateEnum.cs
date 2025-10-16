namespace webbhelpuf.Enums;

public enum CustomerStateEnum
{
    none,
    
    hasRegistered,//registrerat, inte scannat, inte betalat
    
    hasScanned,//registrerat, scannat qr-kod, inte betalat
    
    hasPaid//registrerat, scannat, betalat
}