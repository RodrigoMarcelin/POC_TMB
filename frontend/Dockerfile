# Etapa 1: Construção
FROM node:23.11 AS build  


WORKDIR /app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

RUN npm run build

FROM node:23.11   

WORKDIR /app

COPY --from=build /app /app

RUN npm install --only=production

EXPOSE 5173

CMD ["npm", "run", "dev"]