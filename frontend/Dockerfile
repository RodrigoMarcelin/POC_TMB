# Etapa 1: Construção
FROM node:23.11 AS build  

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

FROM node:23.11   

WORKDIR /app

COPY --from=build /app .

EXPOSE 5173

CMD ["npm", "run", "dev", "--", "--host"]