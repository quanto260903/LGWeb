'use client';
import Image from "next/image";
import '../app/css/components/_buttons.scss';
import './../../src/app/css/pages/_services.scss';
import techImage from './../../src/app/images/backgroundservices.png'
import iconTech from './../../src/app/images/Tech-Vector.png'
import React, { useState, useEffect } from 'react';
import { BlogCategoryType } from '@/type/BlogCategory';
import { GetTechnologies } from '@/api/service';

export default function Technologies() {
  const [data, setData] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await GetTechnologies();
      setData(result);
    };
    loadData();
  }, []);
  return (
    <div className="technologies-section">
     
        <h1 className="tl6">Technologies</h1>
        <div className="technologies-grid">
          {data && data.map((item, index) => (
            <span key={index}>
              <Image src={iconTech} alt="icon" />
              {item.name}
            </span>
          ))}
        </div>
      </div>
  );
}
