```batch
REM https://vite.dev/
REM criar o projeto
npm create vite@latest

REM instalar as dependencias
npm install

REM https://tailwindcss.com/
REM pluggin para o vscode
REM https://marketplace.visualstudio.com/items?itemName=bradlc.vscode-tailwindcss
REM https://ui.shadcn.com/
REM instalar o tailwind + shadcnui
npm install tailwindcss @tailwindcss/vite
REM atualizar src/index.css com o conteudo abaixo
@import "tailwindcss";
REM atualizar tsconfig.json com o conteudo abaixo
"compilerOptions": {
  "baseUrl": ".",
  "paths": {
    "@/*": ["./src/*"]
  }
}
REM atualizar tsconfig.app.json com o conteudo abaixo
"baseUrl": ".",
"paths": {
  "@/*": [
    "./src/*"
  ]
}
REM instalar types do node
npm install -D @types/node
REM atualizar vite.config.ts com o conteudo abaixo
import path from "path"
import tailwindcss from "@tailwindcss/vite"
REM //
plugins: [react(), tailwindcss()],
resolve: {
  alias: {
    "@": path.resolve(__dirname, "./src"),
  },
},
REM instalar o shadcnui
npx shadcn@latest init
REM exemplo de uso para botão
npx shadcn@latest add

REM ícones
REM https://lucide.dev/

REM dev mode
npm run dev

REM components
npx shadcn@latest add table
npx shadcn@latest add dialog
npx shadcn@latest add checkbox
npx shadcn@latest add switch
npx shadcn@latest add input
npx shadcn@latest add label
npx shadcn@latest add alert-dialog
```
