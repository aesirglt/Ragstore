'use client';

import React, { createContext, useContext, useState, ReactNode } from 'react';
import { ServerResume } from '../hooks/useServers';

type ServerContextType = {
  currentServer: ServerResume | null;
  setCurrentServer: (server: ServerResume) => void;
};

const ServerContext = createContext<ServerContextType | undefined>(undefined);

export function ServerProvider({ children }: { children: ReactNode }) {
  const [currentServer, setCurrentServer] = useState<ServerResume | null>(null);

  return (
    <ServerContext.Provider value={{ currentServer, setCurrentServer }}>
      {children}
    </ServerContext.Provider>
  );
}

export function useServer() {
  const context = useContext(ServerContext);
  if (context === undefined) {
    throw new Error('useServer must be used within a ServerProvider');
  }
  return context;
} 