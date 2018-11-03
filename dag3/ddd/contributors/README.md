# Contributor

I katalogdomenemodellen har vi en klasse `Contributor` som representerer medvirkende i et program. Som man kan se av C#-koden, så er `name` og `role` påkrevd (kan ikke være `null`), mens `givenName` og `familyName` er valgfrie. Hvordan ville du representert noe tilsvarende med algebraiske datatyper i F#?

En ting som ikke inngår i domenemodellen er at medvirkende som har rolle "barn" eller "anonym" ikke skal vises. Disse blir pr i dag filtrert ut i Steinbrudd. Reglene er ganske grovkornet da rollenavn er fritekstfelter, og det er bedre med falske positive enn falske negative når det gjelder å filtrere bort medvirkende som ikke skal vises. Derfor blir alle medvirkende med rolle som inneholder tekststrengen "barn" filtrert ut, tilsvarende med rolle som inneholder tekststrengen "anonym". 

Kan typesystemet til F# hjelpe oss med å implementere disse reglene også?
