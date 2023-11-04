# Obtenir l'objet de la batterie
$batterie = Get-WmiObject -Class Win32_Battery

# Vérifier si la batterie est présente
if ($batterie) {
    # Vérifier si le temps restant est disponible
    if ($batterie.EstimatedChargeRemaining -eq 255) {
        Write-Host "Le temps restant est indéterminé."
    } elseif ($batterie.EstimatedChargeRemaining -eq 100) {
        Write-Host "La batterie est complètement chargée."
    } else {
        $tempsRestant = [TimeSpan]::FromMinutes($batterie.EstimatedRunTime)
        Write-Host "Temps de batterie restant : $($tempsRestant.Hours) heures et $($tempsRestant.Minutes) minutes"
    }
} else {
    Write-Host "Aucune information sur la batterie n'est disponible sur cet appareil."
}
