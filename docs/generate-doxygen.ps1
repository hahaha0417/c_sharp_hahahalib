param(
    [string]$DoxyfilePath = ".\Doxyfile"
)

if (-not (Get-Command doxygen -ErrorAction SilentlyContinue)) {
    Write-Error "doxygen command not found. Please install Doxygen first."
    exit 1
}

if (-not (Test-Path -LiteralPath $DoxyfilePath)) {
    Write-Error "Doxyfile not found: $DoxyfilePath"
    exit 1
}

Write-Host "Generating hahahalib Doxygen docs..."
doxygen $DoxyfilePath

if ($LASTEXITCODE -ne 0) {
    Write-Error "Doxygen failed. Check the output above."
    exit $LASTEXITCODE
}

Write-Host "Done. Open docs/output/doxygen/html/index.html"
