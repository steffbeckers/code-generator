# build stage
FROM node as build-stage

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build.prod

# production stage
FROM nginx as production-stage

COPY --from=build-stage /app/dist/code-generator /usr/share/nginx/html

COPY --from=build-stage /app/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
