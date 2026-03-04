# --- Stage 1: Install dependencies ---
FROM node:22-alpine AS deps
RUN apk add --no-cache libc6-compat
WORKDIR /app

# Copy package files for caching
COPY client/package*.json ./
RUN npm install --omit-dev

# --- Stage 2: Build the application ---
FROM node:22-alpine AS builder
WORKDIR /app

# Copy files from deps stage
ARG NEXT_PUBLIC_NOTIFY_URL
ENV NEXT_PUBLIC_NOTIFY_URL=$NEXT_PUBLIC_NOTIFY_URL

COPY --from=deps /app/node_modules ./node_modules
COPY client/ .

# Set environment variables for build time
# (Next.js needs these to bake them into the client bundle)
ENV NEXT_TELEMETRY_DISABLED=1
ENV NODE_ENV=production

RUN npm run build

# --- Stage 3: Runner ---
FROM node:22-alpine AS runner
WORKDIR /app

ENV NODE_ENV=production
ENV NEXT_TELEMETRY_DISABLED=1


# Create a non-root user for security
RUN addgroup --system --gid 1001 nodejs
RUN adduser --system --uid 1001 nextjs

# Copy only necessary files from builder
COPY --from=builder /app/public ./public

# Set the correct permission for prerender cache
RUN mkdir .next
RUN chown nextjs:nodejs .next

# Automatically leverage output traces to reduce image size
# https://nextjs.org/docs/advanced-features/output-file-tracing
COPY --from=builder --chown=nextjs:nodejs /app/.next/standalone ./
COPY --from=builder --chown=nextjs:nodejs /app/.next/static ./.next/static

USER nextjs

EXPOSE 3000

ENV PORT=3000
ENV HOSTNAME="0.0.0.0"

# server.js is created by next build when using standalone output
CMD ["node", "server.js"]