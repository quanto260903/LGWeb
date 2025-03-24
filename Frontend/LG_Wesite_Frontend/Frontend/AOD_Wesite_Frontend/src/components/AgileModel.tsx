"use client";
import React, { useState, useEffect } from 'react';
import Image from "next/image";

import iconDropdown from './../../src/app/images/iconDropdown.png';
import '../app/css/pages/_methodresource.scss';

import { BlogCategoryType } from '@/type/BlogCategory';
import { AgileType, ActiveAccord } from '@/type/BlogCategory';
import { GetAgileModel } from '@/api/methodresource';

export default function AgileModel() {
    const [activeAccordion, setActiveAccordion] = useState(null);
    const [data, setData] = useState<AgileType>();
    const [activeAccord, setActiveAccord] = useState<ActiveAccord[]>();

    useEffect(() => {
        const loadData = async () => {
            const result = await GetAgileModel();
            setData(result);
            InitActive(result?.listBlogCategory)

        };
        loadData();
    }, []);

    const InitActive = (data : BlogCategoryType[]) => {
        var listAccord : ActiveAccord[] = [];
        if (data != undefined && data != null) {
            data.forEach((item, index)=>{
                var itemAdd = {
                    index: index,
                    isActive: false
                }
                listAccord.push(itemAdd)
            })
            setActiveAccord(listAccord);
        }
    };

    const toggleAccordion = (indexParam: number) => {
        const updatedAccord = activeAccord?.map((item, index) => ({
            ...item,
            isActive: index === indexParam ? !item.isActive : false,
        }));
        setActiveAccord(updatedAccord);
    };

    return (
        <div className="agile-container wrp">
            <h3 className="tl">\ Method & Resources \</h3>
            <h1 className="tl2">Agile model</h1>
            <div className="agile-model">
                <div className="content-left">
                    {data && data.listBlogCategory && data.listBlogCategory.map((item, index) => (
                        <div key={index} className="accordion">
                            <div className="accordion-item">
                                <button onClick={() => toggleAccordion(index)}
                                    className={`tl41 ${activeAccord?.filter(p=> p.index == index)[0].isActive ? "activeAcc" : ""}`}>
                                    {item.name}
                                    <Image
                                        src={iconDropdown}
                                        alt="Software development"
                                    />
                                </button>
                                {
                                activeAccord?.filter(p=> p.index == index)[0].isActive && (
                                    <div className="accordion-content">
                                        <p>
                                            {item.description}
                                        </p>
                                    </div>
                                )}
                            </div>
                        </div>
                    ))}
            </div>
            <div className="content-right">
                {
                    data && <img
                        src={data.imageUrl || '/picture/NoImage.png'}
                        style={{ width: '400', height: 'auto' }}
                        alt={data.imageUrl || 'Image'}
                    />
                }
            </div>
        </div>
        </div>
    );
}
