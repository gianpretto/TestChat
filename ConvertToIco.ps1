
Add-Type -AssemblyName System.Drawing

$source = "c:\Users\Gian\.gemini\antigravity\playground\deep-expanse\WpfChatApp\Resources\AppIcon.png"
$dest = "c:\Users\Gian\.gemini\antigravity\playground\deep-expanse\WpfChatApp\Resources\AppIcon.ico"

if (Test-Path $source) {
    Write-Host "Found Source: $source"
    $bmp = [System.Drawing.Bitmap]::FromFile($source)
    $handle = $bmp.GetHicon()
    $icon = [System.Drawing.Icon]::FromHandle($handle)
    
    $fs = new-object System.IO.FileStream($dest, [System.IO.FileMode]::Create)
    $icon.Save($fs)
    $fs.Close()
    $fs.Dispose()
    
    # Clean up (Note: GetHicon handle technically needs DestroyIcon via P/Invoke but for this script we skip it)
    $icon.Dispose()
    $bmp.Dispose()
    
    Write-Host "Created: $dest"
} else {
    Write-Error "Source file not found: $source"
}
